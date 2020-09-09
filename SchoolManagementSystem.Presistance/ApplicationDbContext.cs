
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Application.Common;
using SchoolnManagementSystem.Domain.Common;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Presistance
{
    public class ApplicationDbContext:IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>,IApplicationDbContext
    {
        private readonly IDateTimeServices _datetime;
        private readonly IUserAccessor _user;

         public ApplicationDbContext(IDateTimeServices datetime, IUserAccessor user, DbContextOptions options):base(options)
        {
            _datetime = datetime;
            _user = user;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _user.UserId;
                        entry.Entity.CreatedTs = _datetime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _user.UserId;
                        entry.Entity.LastModifiedTs = _datetime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(builder);
        }

    }
}
