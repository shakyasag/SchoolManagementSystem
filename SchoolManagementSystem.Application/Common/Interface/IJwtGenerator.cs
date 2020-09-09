using SchoolManagementSystem.Application.Auth.UserLogin.Command;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common.Interface
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(User user);
    }
}
