using EducationPlatform.Domain.Commons;
using EducationPlatform.Domain.Configuration;
using EducationPlatform.Domain.Entities.Courses;
using EducationPlatform.Service.DTOs;
using EducationPlatform.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EducationPlatform.Api.Controllers
{
    #pragma warning disable
    [ApiController]
    [Route("Api/[controller]")]
    public class CourseController : ControllerBase
    {
        private ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Course>>>> GetAll([FromQuery]PaginationParams @params)
        {
            var result = courseService.GetAll(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseCreationViewModel courseView)
        {
            var result = await courseService.CreateAsync(courseView);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);

        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<Course>>> Update(long id, [FromForm]CourseCreationViewModel courseView)
        {
            var result = await courseService.Update(id, courseView);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);

        }

        [HttpDelete]
        public async Task<ActionResult<BaseResponse<Course>>> Delete([FromQuery]long id)
        {
            var result = await courseService.Delete(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
