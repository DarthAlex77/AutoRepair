using System;
using System.Text.RegularExpressions;
using AutoRepair.ViewModel;
using FluentValidation;

namespace AutoRepair.Validators
{
    public class CarValidator : AbstractValidator<CarEditWindowsViewModel>
    {
        public CarValidator()
        {

            RuleFor(x => x.CarModel).NotEmpty();
            RuleFor(x => x.CarManufacturer).NotEmpty();
            RuleFor(x => x.Color.ToString()).Matches(new Regex(@"^#FF(?:[0-9a-fA-F]{6})$")).NotNull();
            RuleFor(x => x.CarProduceYear).InclusiveBetween("1990",DateTime.Today.Year.ToString()).NotEmpty();
            RuleFor(x => x.CarNumber).Matches(new Regex(@"^[АВЕКМНОРСТУХ]\d{3}(?<!000)[АВЕКМНОРСТУХ]{2}\d{2,3}$")).NotEmpty();
            RuleFor(x => x.CarVin)
                   .Matches(new Regex(
                            @"^(?<wmi>[A-HJ-NPR-Z\d]{3})(?<vds>[A-HJ-NPR-Z\d]{5})(?<check>[\dX])(?<vis>(?<year>[A-HJ-NPR-Z\d])(?<plant>[A-HJ-NPR-Z\d])(?<seq>[A-HJ-NPR-Z\d]{6}))$")).NotEmpty();
            RuleFor(x => x.CarOwner).NotNull();
        }
    }
}