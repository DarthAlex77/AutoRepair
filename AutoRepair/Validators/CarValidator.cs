using System;
using System.Text.RegularExpressions;
using AutoRepair.Model;
using AutoRepair.ViewModel;
using FluentValidation;

namespace AutoRepair.Validators
{
    public class CarValidator : AbstractValidator<CarEditWindowsViewModel>
    {
        public CarValidator()
        {
            RuleFor(x => x.Car.CarModel.Model).NotEmpty();
            RuleFor(x => x.Car.CarModel.Manufacturer).NotEmpty();
            RuleFor(x => x.Car.Color.ToString()).Matches(new Regex(@"^#FF(?:[0-9a-fA-F]{6})$")).NotNull();
            RuleFor(x => x.Car.CarProduceYear).InclusiveBetween("1990", DateTime.Today.Year.ToString()).NotEmpty();
            RuleFor(x => x.Car.CarNumber).Matches(new Regex(@"^[АВЕКМНОРСТУХ]\d{3}(?<!000)[АВЕКМНОРСТУХ]{2}\d{2,3}$")).NotEmpty();
            RuleFor(x => x.Car.CarVin)
                   .Matches(new Regex(
                            @"^(?<wmi>[A-HJ-NPR-Z\d]{3})(?<vds>[A-HJ-NPR-Z\d]{5})(?<check>[\dX])(?<vis>(?<year>[A-HJ-NPR-Z\d])(?<plant>[A-HJ-NPR-Z\d])(?<seq>[A-HJ-NPR-Z\d]{6}))$"))
                   .NotEmpty();
            RuleFor(x => x.Car.CarOwner).NotNull();
        }
    }
}