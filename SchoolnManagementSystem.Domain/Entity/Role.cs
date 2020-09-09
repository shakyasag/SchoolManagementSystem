using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolnManagementSystem.Domain.Entity
{
    public class Role:IdentityRole<int>
    {
        public virtual ICollection<UserRole>  UserRole { get; set; }

    }
}
