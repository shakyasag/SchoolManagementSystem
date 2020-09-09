using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Application.Common.Interface;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Auth.UserLogin.Command
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticateResponse>
    { private readonly IJwtGenerator _jwt;
        private readonly SignInManager<User> _signinmanager;
        private readonly UserManager<User> _usermanager;
        public LoginQueryHandler(IJwtGenerator jwt, SignInManager<User> signinmanager, UserManager<User> usermanager)
        {
            _jwt = jwt;
            _signinmanager = signinmanager;
            _usermanager = usermanager;
        }
        public async Task<AuthenticateResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            
            throw new NotImplementedException();
        }
    }
}
