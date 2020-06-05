using System.Text.RegularExpressions;
using AutoRepair.ViewModel;
using FluentValidation;

namespace AutoRepair.Validators
{
    public class ClientValidator:AbstractValidator<ClientEditWindowViewModel>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Client.FirstName).NotEmpty();
            RuleFor(x => x.Client.LastName). NotEmpty();
            RuleFor(x => x.Client.PersonalId). Matches(new Regex(@"\d{4}\s\d{6}")).NotEmpty();
            RuleFor(x => x.Client.PhoneNumber).Matches(new Regex(@"\+?\d+([\(\s\-]?\d+[\)\s\-]?[\d\s\-]+)?")).NotEmpty();
            RuleFor(x => x.Client.Address).NotEmpty();
        }
    }
}