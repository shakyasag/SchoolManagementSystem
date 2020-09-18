using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Common.AutoMapper
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
