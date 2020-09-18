
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common.Interface
{
    public interface IRefreshTokens
    {
        Task<RefreshToken> generateRefreshToken(string ipAddress);
    }
}
