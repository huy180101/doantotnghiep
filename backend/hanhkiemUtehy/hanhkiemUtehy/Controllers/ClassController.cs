using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Model;
using hanhkiemUtehy.Repository;
using Microsoft.AspNetCore.Mvc;

namespace hanhkiemUtehy.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClassRepository _classRepository;

        public ClassController(IClassRepository classRepository, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _classRepository = classRepository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> ClassCreate([FromBody] Class model)
        {
            try
            {
                var response = await this._classRepository.ClassCreate(model);
                return Ok(new ResponseSingleContentModel<Class>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpPost("modify")]
        public async Task<IActionResult> ClassModify([FromBody] Class model)
        {
            try
            {
                var response = await this._classRepository.ClassModify(model);
                return Ok(new ResponseSingleContentModel<Class>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpGet("delete")]
        public async Task<IActionResult> ClassDelete(long class_idl)
        {
            try
            {
                var response = await this._classRepository.ClassDelete(class_idl);
                return Ok(new ResponseSingleContentModel<bool>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpGet("add-teacher")]
        public async Task<IActionResult> ClassAddTeacher(long class_id, long teacher_idl)
        {
            try
            {
                var response = await this._classRepository.ClassAddTeacher(class_id, teacher_idl);
                return Ok(new ResponseSingleContentModel<Class>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpGet("list")]
        public async Task<IActionResult> ClassList()
        {
            try
            {
                var response = await this._classRepository.ClassList();
                return Ok(new ResponseSingleContentModel<List<ClassModel>>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpGet("detail-admin")]
        public async Task<IActionResult> ClassDetailAdmin(long class_id)
        {
            try
            {
                var response = await this._classRepository.ClassDetailAdmin(class_id);
                return Ok(new ResponseSingleContentModel<ClassModel>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpGet("detail")]
        public async Task<IActionResult> ClassDetail(long teacher_id)
        {
            try
            {
                var response = await this._classRepository.ClassDetail(teacher_id);
                return Ok(new ResponseSingleContentModel<ClassModel>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpPost("add-student-to-class")]
        public async Task<IActionResult> AddStudentToClass([FromBody] List<Student_User> model)
        {
            try
            {
                var response = await this._classRepository.AddStudentToClass(model);
                return Ok(new ResponseSingleContentModel<List<Student_User>>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpGet("remove-student")]
        public async Task<IActionResult> RemoveStudent(long student_id)
        {
            try
            {
                var response = await this._classRepository.RemoveStudent(student_id);
                return Ok(new ResponseSingleContentModel<bool>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception)
            {
                return this.RouteToInternalServerError();
            }
        }

    }
}
