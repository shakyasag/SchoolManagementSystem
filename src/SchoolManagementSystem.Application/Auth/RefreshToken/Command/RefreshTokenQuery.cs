using MediatR;
using SchoolManagementSystem.Application.Auth.UserLogin.LoginDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Auth.RefreshToken.Command
{
   public  class RefreshTokenQuery:IRequest<AuthenticateResponses>
    {
        public  string RefreshToken { get; set; }
        public string IP { get; set; }
    }
}
