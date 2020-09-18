using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Application.Auth.RefreshToken.Command;
using SchoolManagementSystem.Application.Auth.UserLogin.LoginDto;

namespace SchoolManagementSystem.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticateResponses>> Login(LoginQuery query)
        {
            query.IP = ipAddress();
            var querys= await Mediator.Send(query);
            if(querys!=null)
                setTokenCookie(querys.RefreshToken);
            return querys;


        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponses>>RefreshToken()
        {
           
            var result = await Mediator.Send(new RefreshTokenQuery {IP = ipAddress()
            });
            if (result != null)
                setTokenCookie(result.RefreshToken);
            return result;


        }

        
        [HttpPost("ipAddress")]
        public async Task<ActionResult<bool>> RevokeToken(RevokeTokenQuery query)
        {
          
            // accept token from request body or cookie
            var token = query.Token ?? Request.Cookies["refreshToken"];
            query.Token = token;
            query.IP = ipAddress();
            var result = await Mediator.Send(query);
            if(!result)
                return NotFound(new { message = "Token not found" });
            return result;


        }
        //  helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
