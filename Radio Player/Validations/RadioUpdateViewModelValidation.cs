using FluentValidation;
using Radio_Player.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Validations
{
    public class RadioUpdateViewModelValidation : AbstractValidator<RadioUpdateViewModel>
    {
        public RadioUpdateViewModelValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Category).NotNull().NotEmpty();
            RuleFor(x => x.RadioConnection).NotEmpty().NotNull();           
        }
    }
}
