using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolManagementSystem.Application.Auth.UserLogin.LoginDto;
using SchoolManagementSystem.Application.Common;
using SchoolManagementSystem.Application.Common.Interface;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Auth.RefreshToken.Command
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, AuthenticateResponses>
    {
        private readonly IJwtGenerator _jwt;
        private readonly SignInManager<User> _signinmanager;
        private readonly UserManager<User> _usermanager;
        private readonly IApplicationDbContext _db;
        private readonly IRefreshTokens _token;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public RefreshTokenQueryHandler(IApplicationDbContext db, IRefreshTokens token, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<User> usermanager, IJwtGenerator jwt)
        {
            _db = db;
            _jwt = jwt;
            _token = token;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _usermanager = usermanager;
        }
        public async  Task<AuthenticateResponses> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
            var user =  _usermanager.Users.SingleOrDefault(x => x.RefreshTokens.Any(t => t.Token ==token));
            if (user == null) return null;

            var refreshToken =user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = await _token.generateRefreshToken(request.IP);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = request.IP;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            await _usermanager.UpdateAsync(user);
            await _db.SaveChangesAsync(cancellationToken);

            // generate new jwt
            var jwtToken = await _jwt.CreateToken(user);
            var appuser = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = user.Id,
            


            };

            return new AuthenticateResponses(appuser, jwtToken, newRefreshToken.Token);
        }

       
    }
}
