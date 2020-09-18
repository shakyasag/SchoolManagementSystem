using MediatR;
using SchoolManagementSystem.Application.Auth.UserLogin.LoginDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Auth.RefreshToken.Command
{
    public class RevokeTokenQuery:IRequest<bool>
    {
        public string Token { get; set; }
        public string IP { get; set; }
    }
}
