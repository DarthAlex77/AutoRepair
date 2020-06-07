using System.Text.RegularExpressions;
using AutoRepair.ViewModel;
using FluentValidation;

namespace AutoRepair.Validators
{
    public class ClientValidator:AbstractValidator<ClientEditWindowViewModel>
    {
        public ClientValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Не может быть пустым");
            RuleFor(x => x.LastName). NotEmpty();
            RuleFor(x => x.PersonalId). Matches(new Regex(@"\d{4}\s\d{6}")).NotEmpty();
            RuleFor(x => x.PhoneNumber).Matches(new Regex(@"\+?\d+([\(\s\-]?\d+[\)\s\-]?[\d\s\-]+)?")).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}