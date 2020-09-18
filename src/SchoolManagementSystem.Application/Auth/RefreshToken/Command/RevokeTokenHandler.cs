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
    public class RevokeTokenHandler : IRequestHandler<RevokeTokenQuery,bool>
    {
        private readonly IJwtGenerator _jwt;
        private readonly SignInManager<User> _signinmanager;
        private readonly UserManager<User> _usermanager;
        private readonly IApplicationDbContext _db;
        private readonly IRefreshTokens _token;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public RevokeTokenHandler(IApplicationDbContext db, IRefreshTokens token, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<User> usermanager, IJwtGenerator jwt)
        {
            _db = db;
            _jwt = jwt;
            _token = token;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _usermanager = usermanager;
        }

        public async Task<bool> Handle(RevokeTokenQuery request, CancellationToken cancellationToken)
        {
            var user = _usermanager.Users.SingleOrDefault(x => x.RefreshTokens.Any(t => t.Token == request.Token));
              if (user == null) return false;
            var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);
            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = request.IP;
           await _usermanager.UpdateAsync(user);
           await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
        


    }
}
