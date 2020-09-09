using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagementSystem.Application.Auth.UserLogin.Command
{
    public class LoginQueryValidation: AbstractValidator<LoginQuery>
    {

         public LoginQueryValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName Is Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Is Required");
        }
    }
}
