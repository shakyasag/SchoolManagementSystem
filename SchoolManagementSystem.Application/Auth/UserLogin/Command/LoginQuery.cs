using MediatR;
using SchoolManagementSystem.Application.Auth.UserLogin.LoginDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Auth.UserLogin.LoginDto
{
    public class LoginQuery:IRequest<AuthenticateResponses>
    {
         public string UserName { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
    }
}
