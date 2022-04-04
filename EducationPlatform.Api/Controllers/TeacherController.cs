using EducationPlatform.Domain.Commons;
using EducationPlatform.Domain.Configuration;
using EducationPlatform.Domain.Entities.Teachers;
using EducationPlatform.Service.DTOs;
using EducationPlatform.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationPlatform.Api.Controllers
{
# pragma warning disable 
    [ApiController]
    [Route("Api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private ITeacherService teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<Teacher>>> GetAll([FromQuery]PaginationParams @params)
        {
            var result = teacherService.GetAll(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Teacher>>> Create([FromForm]TeacherCreationViewModel teacherView)
        {
            var result = await teacherService.CreateAsync(teacherView);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<Teacher>>> Update(long id, [FromForm]TeacherCreationViewModel teacherView)
        {
            var result = await teacherService.Update(id, teacherView);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResponse<Teacher>>> Delete(long id)
        {
            var result = await teacherService.Delete(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
