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
    [Route("api/admin")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> UserCreate([FromBody] UserModel model)
        {
            try
            {
                int checkUser = await _userRepository.CheckUserExists(model.username, model.email);
                if (checkUser > 0)
                {
                    return Ok(new ResponseSingleContentModel<string>
                    {
                        StatusCode = 400,
                        Message = "Tài khoản hoặc email đã được đăng ký, vui lòng kiểm tra lại",
                        Data = string.Empty
                    });
                }
                var response = await this._userRepository.UserCreate(model);
                return Ok(new ResponseSingleContentModel<UserModel>
                {
                    StatusCode = 200,
                    Message = "",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return this.RouteToInternalServerError();
            }
        }
        [HttpPost("modify")]
        public async Task<IActionResult> UserModify([FromBody] UserModel userupdate)
        {
            try
            {

                var response = await this._userRepository.UserModify(userupdate);
                return Ok(new ResponseSingleContentModel<UserModel>
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
        public async Task<IActionResult> List()
        {
            try
            {
                List<UserModel> Data = await _userRepository.UserList();
                return Ok(new ResponseSingleContentModel<List<UserModel>>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = Data
                });
            }
            catch (Exception)
            {
                return Ok(new ResponseSingleContentModel<UserModel>
                {
                    StatusCode = 500,
                    Message = "Có lỗi xảy ra trong quá trình xử lý",
                    Data = new()
                });
            }

        }
        [HttpGet("user")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var response = await _userRepository.UserGetById(id);
                return Ok(new ResponseSingleContentModel<UserModel>
                {
                    StatusCode = 200,
                    Message = "",
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
                var user = await _userRepository.CheckUser(login.username);
                if (user != null)
                {

                    int checkAccount = _userRepository.Authenticate(login);
                    UserTokenModel userAuthen = new();
                    if (checkAccount == 1)
                    {
                        ClaimModel claim = new ClaimModel
                        {
                            email = user.email,
                            full_name = user.full_name,
                            id = user.id,
                            username = user.username,
                        };
                        string tokenString = GenerateToken(claim);
                        userAuthen.token = tokenString;
                        userAuthen.id = user.id;
                        userAuthen.is_admin = true;
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
              expires: DateTime.Now.AddHours(9),
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
