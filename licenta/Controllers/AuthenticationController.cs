using AutoMapper;
using licenta.Models.Authentication;
using licenta.Services.Teachers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace licenta.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthenticationController(ITeacherRepository teacherRepository, IMapper mapper, IConfiguration config)
        {
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(LoginDto credentials)
        {
            if (credentials != null)
            {
                var userId = await ValidateUserCredentials(credentials.Email, credentials.Password);
                if (userId == null)
                {
                    return Unauthorized();
                }

                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userId.ToString()));

                var jwtToken = new JwtSecurityToken(
                   _config["Authentication:Issuer"],
                   _config["Authentication:Audience"],
                   claimsForToken,
                   DateTime.UtcNow,
                   DateTime.UtcNow.AddHours(8),
                   signingCredentials);

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return Ok(tokenToReturn);
            }

            return BadRequest("Send some data as well");
        }


        private async Task<Guid?> ValidateUserCredentials(string email, string password)
        {
            var id = await _teacherRepository.Exists(email, password);
            if (id != null)
            {
                return id;
            }
            return null;
        }
    }
}
