using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Application.Common.Interface;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common
{
   public  interface IApplicationDbContext:IDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
