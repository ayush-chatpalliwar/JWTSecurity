using JWTSecurity.Data;
using JWTSecurity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public SecurityDBContext db;
        public LoginController(IConfiguration config)
        {
            _config = config;
            db = new SecurityDBContext();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(UserModel login)
        {
            User user = null;

            if (db.Users.Any(x => x.UserName == login.Username && x.Password == login.Password && x.Role == "Admin"))
            {
                user = new User{ UserName = login.Username, Password = login.Password, Role = "Admin" };

            }
            else if (db.Users.Any(x => x.UserName == login.Username && x.Password == login.Password && x.Role == "Manager"))
            {
                user = new User { UserName = login.Username, Password = login.Password, Role = "Manager" };

            }
            //if (login.Username == "Ayush" && login.Password =="ayush123")
            //{
            //    user = new UserModel { Username = "Ayush", Email = "ayush@gmail.com", Password="ayush123" };
            //}
            //else if (login.Username == "Yogesh" && login.Password == "yogesh123")
            //{
            //    user = new UserModel { Username = "Yogesh", Email = "yogesh@gmail.com", Password = "yogesh123" };
            //}
            return user;
        }
    }
}
