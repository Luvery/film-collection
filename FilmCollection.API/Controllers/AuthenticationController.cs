using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FilmCollection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration m_config;

        public AuthenticationController(IConfiguration config)
        {
            m_config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Authenticate()
        {
            var token = BuildToken("admin", "1");
            return Ok(new { Token = token});
        }

        private string BuildToken(string roleName, string userId)
        {
            var jwtOptions = m_config.GetSection("JwtOptions");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = jwtOptions["Audience"],
                Issuer = jwtOptions["Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId ),
                    new Claim(ClaimTypes.Role, roleName)
                }),
                Expires = DateTime.Now.AddMinutes(double.Parse(jwtOptions["ValidForMinutes"])),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
