using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Presistance.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(u => u.User)
                .WithMany(u => u.UserRole)
                .HasForeignKey(u => u.UserId)
                 .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne(r => r.Role)
                .WithMany(r => r.UserRole)
                .HasForeignKey(r => r.RoleId)
                 .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

        }
    }
}
