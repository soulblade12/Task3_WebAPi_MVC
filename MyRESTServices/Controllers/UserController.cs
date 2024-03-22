using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Helpers;
using MyRESTServices.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyRESTServices.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBLL _userBLL;
        private readonly AppSettings _appSettings;
        public UserController(IUserBLL userBLL, IOptions<AppSettings> appSettings)
        {
            _userBLL = userBLL;
            _appSettings = appSettings.Value;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            var results = await _userBLL.GetAll();
            return results;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("Roles")]
        public async Task<IEnumerable<UserDTO>> GetRoles()
        {
            var results = await _userBLL.GetAllWithRoles();
            return results;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post(UserCreateDTO userCreate)
        {
            if (userCreate == null)
            {
                return BadRequest();
            }

            try
            {
                await _userBLL.Insert(userCreate);
                return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{name}")]
        public async Task<IActionResult> GetbyUsername(string name)
        {
            var result = await _userBLL.GetByUsername(name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var getLogin = await _userBLL.LoginMVC(loginDTO);

                List<Claim> claims = new List<Claim>();
                foreach (var role in getLogin.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var userWithToken = new UserWithToken
                {
                    Token = tokenHandler.WriteToken(token)
                };
                return Ok(userWithToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
