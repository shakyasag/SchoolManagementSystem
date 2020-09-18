using AutoMapper;
using SchoolManagementSystem.Application.Auth.RefreshToken;
using SchoolManagementSystem.Application.Common.AutoMapper;
using SchoolnManagementSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SchoolManagementSystem.Application.Auth.UserLogin.LoginDto
{
    public class UserDto:IMapFrom<User>
    {
       
        public string UserName { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public List<RefreshTokenDto> RefreshTokens { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
                
                  //.ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))

            //.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            // .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            // .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName));


        }

    }
}
