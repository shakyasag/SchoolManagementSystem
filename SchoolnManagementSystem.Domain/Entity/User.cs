using Microsoft.AspNetCore.Identity;
using SchoolnManagementSystem.Domain.Common;
using SchoolnManagementSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SchoolnManagementSystem.Domain.Entity
{
   public class User:IdentityUser<int>
    {
         public virtual ICollection<UserRole> UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        public Gender Gender { get; set; }
        public string Country { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime CreatedTs { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public DateTime? LastModifiedTs { get; set; }
        public Guid? DeleteBy { get; set; }

        public DateTime? DeleteTs { get; set; }
        [JsonIgnore]
       public List<RefreshToken> RefreshTokens { get; set; }
    }
}
