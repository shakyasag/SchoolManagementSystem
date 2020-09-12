using AutoMapper;
using SchoolManagementSystem.Application.Common;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SchoolManagementSystem.Application.Auth.UserLogin.LoginDto
{
    public class UserDtos : IMapFrom<User>
    {
        public string UserName { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDtos>();

        }

    }
}
