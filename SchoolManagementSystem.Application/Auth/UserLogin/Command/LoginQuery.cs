using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Auth.UserLogin.Command
{
    public class LoginQuery:IRequest<AuthenticateResponse>
    {
         public string UserName { get; set; }
        public string Password { get; set; }
        public string IP { get; set; }
    }
}
