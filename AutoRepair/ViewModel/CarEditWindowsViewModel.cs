using System;
using System.Linq;
using System.Reactive;
using AutoRepair.Model;
using AutoRepair.Validators;
using AutoRepair.View;
using ReactiveUI;
using ReactiveUI.FluentValidation;

namespace AutoRepair.ViewModel
{
    public class CarEditWindowsViewModel : ReactiveValidationObject
    {
        #region Constructors

        public CarEditWindowsViewModel() : base(new CarValidator())
        {
            AddCarCommand      = ReactiveCommand.Create(AddCar, isValid);
            EditCarCommand     = ReactiveCommand.Create(EditCar, isValid);
            SelectOwnerCommand = ReactiveCommand.Create(SelectOwner);
            MessageBus.Current.Listen<bool>  ("AddMode").Subscribe(x => IsAddMode = x);
            MessageBus.Current.Listen<Car>   ("EditCar").Subscribe(x => Car = x);
            MessageBus.Current.Listen<Client>("SelectedClient").Subscribe(x => Car.CarOwner = x);
            Validator();
        }

        #endregion

        #region ValidatorMethod

        private void Validator()
        {
            RaiseValidation(nameof(Car.CarModel.Manufacturer));
            RaiseValidation(nameof(Car.CarModel.Model));
            RaiseValidation(nameof(Car.Color));
            RaiseValidation(nameof(Car.CarProduceYear));
            RaiseValidation(nameof(Car.CarNumber));
            RaiseValidation(nameof(Car.CarVin));
            RaiseValidation(nameof(Car.CarOwner));
        }

        #endregion

        #region AddCarCommand

        public ReactiveCommand<Unit, Unit> AddCarCommand { get; }

        private void AddCar()
        {
            using (AppContext db = new AppContext())
            {
                CarModel carModel =
                        db.CarModels.FirstOrDefault(x => x.Manufacturer == Car.CarModel.Manufacturer && x.Model == Car.CarModel.Model) ??
                        new CarModel(Car.CarModel.Manufacturer, Car.CarModel.Model);
                db.Cars.Add(new Car(carModel, Car.Color, Car.CarProduceYear, Car.CarNumber, Car.CarVin, Car.CarEngineNumber,
                        Car.CarBodyNumber, db.Find<Client>(Car.CarOwner.ClientId)));
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion

        #region EditCarCommand

        public ReactiveCommand<Unit, Unit> EditCarCommand { get; }

        private void EditCar()
        {
            using (AppContext db = new AppContext())
            {
                Car car = db.Cars.Find(Car.CarId);
                CarModel carModel = db.CarModels.FirstOrDefault(x => x.Manufacturer == car.CarModel.Manufacturer && x.Model == car.CarModel.Model) ??
                                    new CarModel(car.CarModel.Manufacturer, car.CarModel.Model);
                Client carOwner = db.Clients.Find(car.CarOwner.ClientId);
                car.CarModel        = carModel;
                car.Color           = car.Color;
                car.CarProduceYear  = car.CarProduceYear;
                car.CarNumber       = car.CarNumber;
                car.CarVin          = car.CarVin;
                car.CarEngineNumber = car.CarEngineNumber;
                car.CarBodyNumber   = car.CarBodyNumber;
                car.CarOwner        = carOwner;
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion

        #region CloseTriggerProperty

        private bool _closeTrigger;

        public bool CloseTrigger
        {
            get => _closeTrigger;
            set => this.RaiseAndSetIfChanged(ref _closeTrigger, value);
        }

        #endregion

        #region SelectOwnerCommand

        public ReactiveCommand<Unit, Unit> SelectOwnerCommand { get; }

        private void SelectOwner()
        {
            new ClientEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(2, "WindowMode");
        }

        #endregion

        #region IsAddModeProperty

        private bool _isAddMode;

        public bool IsAddMode
        {
            get => _isAddMode;
            set => this.RaiseAndSetIfChanged(ref _isAddMode, value);
        }

        #endregion

        #region CarProperty

        private Car _car;

        public Car Car
        {
            get => _car;
            set => this.RaiseAndSetIfChanged(ref _car, value);
        }

        #endregion
    }
}