using FluentValidation;
using Radio_Player.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Validations
{
    public class LoginViewModelValidation:AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidation()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
