using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Application.Auth.UserLogin.Command;
using SchoolManagementSystem.Application.Common.AutoMapper;
using SchoolManagementSystem.Application.Common.Behaviour;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SchoolManagementSystem.Application
{
   public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
          
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(BehaviourValidation<,>));
            return services;
        }
    }
}
