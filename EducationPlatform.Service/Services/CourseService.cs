using AutoMapper;
using EducationPlatform.Data.IRepositories;
using EducationPlatform.Domain.Commons;
using EducationPlatform.Domain.Configuration;
using EducationPlatform.Domain.Entities.Courses;
using EducationPlatform.Service.DTOs;
using EducationPlatform.Service.Extentions;
using EducationPlatform.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        private BaseResponse<Course> response;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, BaseResponse<Course> response, 
            IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.response = response;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CourseCreationViewModel courseView)
        {
            Course CourseExist = await unitOfWork.Courses.GetAsync(
                course => course.Name == courseView.Name && course.TeacherId == courseView.TeacherId);
            if  (CourseExist is not null)
            {
                response.Error = new ErrorResponse(400, "Course already exist");
                return response;
            }

            Course mappedCourse = mapper.Map<Course>(courseView);

            mappedCourse.ImageUrl = await SaveFileAsync(courseView.Image.OpenReadStream(), courseView.Image.FileName);

            var result = await unitOfWork.Courses.CreateAsync(mappedCourse);

            result.ImageUrl = "https://localhost:44354/Images/" + result.ImageUrl;

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<Course>> Delete(long id)
        {
            Course ExistCourse = unitOfWork.Courses.GetAsync(course => course.Id == id).Result;
            if (ExistCourse is null)
            {
                response.Error = new ErrorResponse(404, "Course not found");
                return response;
            }

            response.Data = unitOfWork.Courses.Delete(ExistCourse);
            await unitOfWork.SaveChangesAsync();
            return response; 
        }

        public BaseResponse<IEnumerable<Course>> GetAll(PaginationParams @params, Expression<Func<Course, bool>> predicate = null)
        {
            var response = new BaseResponse<IEnumerable<Course>>();
            response.Data = unitOfWork.Courses.GetAll(predicate);

            if (response.Data is null)
            {
                response.Error = new ErrorResponse(404, "Courses not found");
                return response;
            }

            response.Data = response.Data.ToPagedList(@params);
            return response; 
        }

        public async Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> predicate)
        {
            Course result = await unitOfWork.Courses.GetAsync(predicate);
            if (result is null)
            {
                response.Error = new ErrorResponse(404, "Courses not found");
                return response;
            }

            response.Data = result;
            return response;
        }


        public async Task<BaseResponse<Course>> Update(long id, CourseCreationViewModel courseView)
        {
            Course mappedCourse = mapper.Map<Course>(courseView);

            mappedCourse.ImageUrl = await SaveFileAsync(courseView.Image.OpenReadStream(), courseView.Image.FileName);
            mappedCourse.Id = id;

            mappedCourse.ImageUrl = "https://localhost:44354/Images/" + mappedCourse.ImageUrl;

            response.Data = unitOfWork.Courses.Update(mappedCourse);

            await unitOfWork.SaveChangesAsync();

            return response;
        }
        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("StoragePath:ImageUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");

            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }
    }
}
