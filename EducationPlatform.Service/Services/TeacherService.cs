using AutoMapper;
using EducationPlatform.Data.IRepositories;
using EducationPlatform.Domain.Commons;
using EducationPlatform.Domain.Configuration;
using EducationPlatform.Domain.Entities.Teachers;
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
    public class TeacherService : ITeacherService
    {
        private IUnitOfWork unitOfWork;
        private IWebHostEnvironment env;
        private IConfiguration config;
        private BaseResponse<Teacher> response;
        private IMapper mapper;

        public TeacherService(IUnitOfWork unitOfWork, IWebHostEnvironment env, 
            IConfiguration config, BaseResponse<Teacher> response, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.env = env;
            this.config = config;
            this.response = response;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<Teacher>> CreateAsync(TeacherCreationViewModel teacherView)
        {
            var checkExistTeacher = await unitOfWork.Teachers.GetAsync(teacher => teacher.Phone == teacherView.Phone);
            if (checkExistTeacher is not null)
            {
                response.Error = new ErrorResponse(400, "Teacher already exist");
                return response;
            }
            Teacher teacher = mapper.Map<Teacher>(teacherView);

            string imageName = await SaveFileAsync(teacherView.Image.OpenReadStream(), teacherView.Image.FileName);
            teacher.ImageUrl = "https://course-platform-shax.herokuapp.com/Images/Teachers/" + imageName;

            response.Data = await unitOfWork.Teachers.CreateAsync(teacher);
            await unitOfWork.SaveChangesAsync();

            return response;
        }

        public async Task<BaseResponse<Teacher>> Delete(long id)
        {
            var foundTeacher = await unitOfWork.Teachers.GetAsync(teacher => teacher.Id == id);

            if (foundTeacher is null)
            {
                response.Error = new ErrorResponse(404, "Not Found");
                return response;
            }

            response.Data = unitOfWork.Teachers.Delete(foundTeacher);
            await unitOfWork.SaveChangesAsync();

            return response;
        }

        public BaseResponse<IEnumerable<Teacher>> GetAll(PaginationParams @params, Expression<Func<Teacher, bool>> predicate = null)
        {
            var response = new BaseResponse<IEnumerable<Teacher>>();
            
            response.Data = unitOfWork.Teachers.GetAll(predicate).ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Teacher>> GetAsync(Expression<Func<Teacher, bool>> predicate)
        {
            response.Data = await unitOfWork.Teachers.GetAsync(predicate);
            
            if (response.Data is null)
            {
                response.Error = new ErrorResponse(404, "Not Found");
                return response;
            }

            return response;
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("StoragePath:ImageUrls:ForTeacher").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");

            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }

        public async Task<BaseResponse<Teacher>> Update(long id, TeacherCreationViewModel teacherView)
        {
            Teacher teacher = mapper.Map<Teacher>(teacherView);

            string imageName = await SaveFileAsync(teacherView.Image.OpenReadStream(), teacherView.Image.FileName);
            teacher.ImageUrl = "https://course-platform-shax.herokuapp.com/Images/Teachers/" + imageName;
            teacher.Id = id;
            
            response.Data = unitOfWork.Teachers.Update(teacher);
            await unitOfWork.SaveChangesAsync();

            return response;
        }
    }
}
