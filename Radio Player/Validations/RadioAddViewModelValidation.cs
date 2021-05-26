using FluentValidation;
using Radio_Player.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Validations
{
    public class RadioAddViewModelValidation:AbstractValidator<RadioAddViewModel>
    {
        public RadioAddViewModelValidation()
        {
            RuleFor(x => x.Category).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(x => x.RadioConnection).NotNull().NotEmpty();
        }
    }
}
