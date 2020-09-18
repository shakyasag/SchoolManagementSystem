using Castle.DynamicProxy.Generators.Emitters.CodeBuilders;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Auth.RefreshToken.Command
{
   public class RefreshTokenQueryValidation: AbstractValidator<RefreshTokenQuery>
    {

        public RefreshTokenQueryValidation()
        {
           
            RuleFor(x => x.IP).NotEmpty();
        }
    }
}
