using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Application.Auth.UserLogin.LoginDto;
using SchoolManagementSystem.Application.Common;
using SchoolManagementSystem.Application.Common.Error;
using SchoolManagementSystem.Application.Common.Interface;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Auth.UserLogin.Command
{
  

    public class LoginQueryHandler : IRequestHandler<LoginQuery,AuthenticateResponses>
    {
        private readonly IJwtGenerator _jwt;
        private readonly SignInManager<User> _signinmanager;
        private readonly UserManager<User> _usermanager;
        private readonly IApplicationDbContext _db;
        private readonly IRefreshTokens _token;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public LoginQueryHandler(IJwtGenerator jwt, SignInManager<User> signinmanager, IRefreshTokens token, UserManager<User> usermanager, IApplicationDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _jwt = jwt;
            _signinmanager = signinmanager;
            _usermanager = usermanager;
            _db = db;
            _token = token;
            _mapper = mapper;
            _httpContextAccessor =httpContextAccessor;
        }
        public async Task<AuthenticateResponses> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _usermanager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized,
                      new { error = "UnAuthorize User!! Please try again" });
            //var appuser = _mapper.Map<UserDto>(user);
           
            var result = await _signinmanager
              .CheckPasswordSignInAsync(user, request.Password, false);
            var jwttoken = await _jwt.CreateToken(user);
            var Refreshtoken = await _token.generateRefreshToken(request.IP);
            if (result.Succeeded)
            {
                // save refresh token
                user.RefreshTokens.Add(Refreshtoken);
                await _usermanager.UpdateAsync(user);
                

                await _db.SaveChangesAsync(cancellationToken);
                var appuser = new UserDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Id = user.Id,
                   //RefreshTokens= Refreshtoken


                };

                return new AuthenticateResponses(appuser, jwttoken, Refreshtoken.Token);

            }
           
            throw new RestException(HttpStatusCode.Unauthorized,
                      new { error = "UnAuthorize User !!Please try again" });



        }

        
    }

   
}
