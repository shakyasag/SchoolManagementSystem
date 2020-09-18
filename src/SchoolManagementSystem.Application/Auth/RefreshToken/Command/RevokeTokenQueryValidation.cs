using Castle.Facilities.TypedFactory.Internal;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Auth.RefreshToken.Command
{
   public  class RevokeTokenQueryValidation: AbstractValidator<RevokeTokenQuery>
    {
        public RevokeTokenQueryValidation()
        {
            RuleFor(x => x.IP).Empty().WithMessage("IP addresss is empty");
            RuleFor(x => x.Token).Empty();
        }
    }
}
