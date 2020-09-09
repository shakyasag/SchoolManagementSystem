using SchoolManagementSystem.Application.Auth.UserLogin.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common.Interface
{
    public interface IRefreshToken
    {
        RefreshToken generateRefreshToken(string ipAddress);
    }
}
