using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SchoolManagementSystem.Application.Auth.UserLogin.LoginDto
{
    public class AuthenticateResponses
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponses(UserDto User, string Jwttoken, string Refreshtoken)
        {
            Id =User.Id;
            FirstName = User.FirstName;
            LastName = User.LastName;
            UserName = User.LastName;
            JwtToken = Jwttoken;
            RefreshToken = Refreshtoken;
        }
    }
}
