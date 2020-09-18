using Microsoft.AspNetCore.Http;
using SchoolManagementSystem.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolManagementSystem.Infrastructures.Common
{
    public class UserAccessor: IUserAccessor
    {
        public Guid UserId => GetCurrentUserId();

        private IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            // UserId = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserName => GetCurrentUserName();

        private Guid GetCurrentUserId()
        {
            var IdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");
            if (IdClaim != null)
            {
                return Guid.Parse(IdClaim.Value);
            }
            return Guid.Empty;
        }

        private string GetCurrentUserName()
        {
            var NameClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name");
            if (NameClaim != null)
            {
                return NameClaim.Value;
            }
            return String.Empty;
        }
    }
}
