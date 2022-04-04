using AutoMapper;
using EducationPlatform.Domain.Entities.Courses;
using EducationPlatform.Domain.Entities.Teachers;
using EducationPlatform.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseCreationViewModel, Course>().ReverseMap();
            CreateMap<TeacherCreationViewModel, Teacher>().ReverseMap();
        }
    }
}
