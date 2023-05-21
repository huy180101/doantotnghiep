using hanhkiemUtehy.Entity;
using hanhkiemUtehy.Model;
using hanhkiemUtehy.Model.User;
using hanhkiemUtehy.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hanhkiemUtehy.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _studentRepository = studentRepository;
        }
        [HttpPost("modify")]
        public async Task<IActionResult> Modify([FromBody] Student_User model)
        {
            try
            {

                var response = await this._studentRepository.Modify(model);
                return Ok(new ResponseSingleContentModel<Student_User>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = response
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<IResponseData>
                {
                    StatusCode = 500,
                    Message = "Có lỗi trong quá trình xử lý",
                    Data = null
                });
            }
        }
        [HttpGet("list")]
        public async Task<IActionResult> List(long class_id)
        {
            try
            {
                List<Student_User> Data = await _studentRepository.List(class_id);
                return Ok(new ResponseSingleContentModel<List<Student_User>>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = Data
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<Student_User>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = new()
                });
            }

        }
        [HttpGet("detail")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var user = await _studentRepository.GetById(id);
                return Ok(new ResponseSingleContentModel<Student_User>
                {
                    StatusCode = 200,
                    Message = "Thêm mới thành công",
                    Data = user
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = string.Empty
                });
            }
        } 
        [HttpGet("conduct-form")]
        public async Task<IActionResult> GetFormByStudentId(long student_id)
        {
            try
            {
                var response = await _studentRepository.GetFormByStudentId(student_id);
                return Ok(new ResponseSingleContentModel<Conduct_Form>
                {
                    StatusCode = 200,
                    Message = "Lấy thông tin thành công",
                    Data = response
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = string.Empty
                });
            }
        } 
        [HttpPost("conduct-form-update")]
        public async Task<IActionResult> UpdateForm([FromBody]Conduct_Form model)
        {
            try
            {
                var response = await _studentRepository.UpdateForm(model);
                return Ok(new ResponseSingleContentModel<Conduct_Form>
                {
                    StatusCode = 200,
                    Message = "Cập nhật thành công",
                    Data = response
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = string.Empty
                });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                var user = await _studentRepository.CheckUser(login.username);
                if (user != null)
                {

                    int checkAccount = _studentRepository.Authenticate(login);
                    UserTokenModel userAuthen = new();
                    if (checkAccount == 1)
                    {
                        ClaimModel claim = new()
                        {
                            email = user.email,
                            full_name = user.full_name,
                            id = user.id,
                            username = user.username,
                        };
                        string tokenString = GenerateToken(claim);
                        userAuthen.token = tokenString;
                        userAuthen.id = user.id;
                        userAuthen.class_id = user.class_id;
                        userAuthen.teacher_id = user.teacher_id;
                        userAuthen.is_officer = user.is_officer;
                        userAuthen.username = user.username;
                        userAuthen.full_name = user.full_name;
                        userAuthen.token = tokenString;
                        return Ok(new ResponseSingleContentModel<UserTokenModel>
                        {
                            StatusCode = 200,
                            Message = "Đăng nhập thành công",
                            Data = userAuthen
                        });
                    }
                    else
                    {
                        return Ok(new ResponseSingleContentModel<string>
                        {
                            StatusCode = 500,
                            Message = "Sai tài khoản hoặc mật khẩu",
                            Data = null
                        });
                    }
                }
                else
                {
                    return Ok(new ResponseSingleContentModel<string>
                    {
                        StatusCode = 500,
                        Message = "Tài khoản không tồn tại trong hệ thống",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new ResponseSingleContentModel<string>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = string.Empty
                });
            }
        }
        private string GenerateToken(ClaimModel user)
        {
            var identity = GetClaims(user);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["TokenSettings:Key"]));
            var token = new JwtSecurityToken(
            _configuration["TokenSettings:Issuer"],
             _configuration["TokenSettings:Audience"],
              expires: DateTime.Now.AddDays(9),
              claims: identity,
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
              );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private IEnumerable<Claim> GetClaims(ClaimModel user)
        {
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Typ, user.type.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.username.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.full_name),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim(JwtRegisteredClaimNames.Sid, user.id.ToString())
            };
            return claims;
        }


    }
}
