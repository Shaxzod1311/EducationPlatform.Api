using EducationPlatform.Domain.Commons;
using EducationPlatform.Domain.Configuration;
using EducationPlatform.Domain.Entities.Courses;
using EducationPlatform.Service.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Service.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponse<Course>> CreateAsync(CourseCreationViewModel course);
        Task<BaseResponse<Course>> Update(long id, CourseCreationViewModel course);
        Task<BaseResponse<Course>> Delete(long id);
        Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> predicate);
        BaseResponse<IEnumerable<Course>> GetAll(PaginationParams @params, Expression<Func<Course, bool>> predicate = null);
        Task<string> SaveFileAsync(Stream file, string fileName);


    }
}
