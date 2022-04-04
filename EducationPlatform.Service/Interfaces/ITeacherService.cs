using EducationPlatform.Domain.Commons;
using EducationPlatform.Domain.Configuration;
using EducationPlatform.Domain.Entities.Teachers;
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
    public interface ITeacherService
    {
        Task<BaseResponse<Teacher>> CreateAsync(TeacherCreationViewModel teacherView);
        Task<BaseResponse<Teacher>> GetAsync(Expression<Func<Teacher, bool>> predicate);
        Task<BaseResponse<Teacher>> Delete(long id);
        Task<BaseResponse<Teacher>> Update(long id, TeacherCreationViewModel teacherView);
        BaseResponse<IEnumerable<Teacher>> GetAll(PaginationParams @params, Expression<Func<Teacher, bool>> predicate = null);
        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}
