using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return await Mediator.Send(query);

        }

        // helper methods

        //private void setTokenCookie(string token)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = DateTime.UtcNow.AddDays(7)
        //    };
        //    Response.Cookies.Append("refreshToken", token, cookieOptions);
        //}

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
