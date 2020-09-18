
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystem.Application.Common.Interface;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Infrastructures.Common
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SignInManager<User> _signinmanager;
        private readonly UserManager<User> _usermanager;
        private readonly IConfiguration _config;
        public JwtGenerator(SignInManager<User>signinmanager, UserManager<User> usermanager, IConfiguration config)
        {
            _signinmanager = signinmanager;
            _usermanager = usermanager;
            _config = config;

        }
        public async Task<string> CreateToken(User user)
        {
            var claim = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            var Role = await _usermanager.GetRolesAsync(user);
            foreach(var roles in Role)
            {
                claim.Add(new Claim(ClaimTypes.Role, roles));
            }
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)), SecurityAlgorithms.HmacSha256Signature)

            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendesc);
            return tokenhandler.WriteToken(token);


            throw new NotImplementedException();
        }
    }
}
