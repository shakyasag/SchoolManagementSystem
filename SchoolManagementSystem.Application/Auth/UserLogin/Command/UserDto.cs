using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SchoolManagementSystem.Application.Auth.UserLogin.Command
{
    public class UserDto
    {
        public string UserName { get; set; }
         public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }


    }
}
