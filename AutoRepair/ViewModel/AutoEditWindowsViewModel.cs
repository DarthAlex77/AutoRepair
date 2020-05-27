﻿using System;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using AutoRepair.Model;
using AutoRepair.View;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class AutoEditWindowsViewModel : ReactiveObject
    {
        #region Constructors

        public AutoEditWindowsViewModel()
        {
            AddCarCommand = ReactiveCommand.Create(AddCar);
            SelectOwnerCommand = ReactiveCommand.Create(SelectOwner);
            MessageBus.Current.Listen<bool>("AddMode").Subscribe(x => IsAddMode = x);
            MessageBus.Current.Listen<int>("EditCarId").Subscribe(SetCarProperties);
            MessageBus.Current.Listen<Client>("SelectedClient").Subscribe(x => CarOwner = x);
        }

        #endregion

        #region SetCarPropertiesMethod

        private void SetCarProperties(int carId)
        {
            Car car;
            using (var db = new AppContext())
            {
                db.CarModels.Load();
                db.Clients.Load();
                car = db.Cars.Find(carId);
            }

            CarManufacturer = car.CarModel.Manufacturer;
            CarModel = car.CarModel.Model;
            Color = car.Color;
            CarProduceYear = car.CarProduceYear;
            CarNumber = car.CarNumber;
            CarVin = car.CarVin;
            CarEngineNumber = car.CarEngineNumber;
            CarBodyNumber = car.CarBodyNumber;
            CarOwner = car.CarOwner;
        }

        #endregion

        #region AddCarCommand

        public ReactiveCommand<Unit, Unit> AddCarCommand { get; }

        private void AddCar()
        {
            using (var db = new AppContext())
            {
                var carModel =
                    db.CarModels.FirstOrDefault(x => x.Manufacturer == CarManufacturer && x.Model == CarModel) ??
                    new CarModel(CarManufacturer, CarModel);
                db.Cars.Add(new Car(carModel, Color, CarProduceYear, CarNumber, CarVin, CarEngineNumber,
                    CarBodyNumber, CarOwner));
            }
        }

        #endregion

        #region SelectOwnerCommand

        public ReactiveCommand<Unit, Unit> SelectOwnerCommand { get; }

        private void SelectOwner()
        {
            new ClientEditWindow().Show();
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

        #region CarProperties

        #region CarManufacturerProperty

        private string _carManufacturer;

        public string CarManufacturer
        {
            get => _carManufacturer;
            set => this.RaiseAndSetIfChanged(ref _carManufacturer, value);
        }

        #endregion

        #region CarModelProperty

        private string _carModel;

        public string CarModel
        {
            get => _carModel;
            set => this.RaiseAndSetIfChanged(ref _carModel, value);
        }

        #endregion

        #region ColorProperty

        private Color _color;

        public Color Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        #endregion

        #region CarProduceYearProperty

        private string _carProduceYear;

        public string CarProduceYear
        {
            get => _carProduceYear;
            set => this.RaiseAndSetIfChanged(ref _carProduceYear, value);
        }

        #endregion

        #region CarNumberProperty

        private string _carNumber;

        public string CarNumber
        {
            get => _carNumber;
            set => this.RaiseAndSetIfChanged(ref _carNumber, value);
        }

        #endregion

        #region CarVINProperty

        private string _carVin;

        public string CarVin
        {
            get => _carVin;
            set => this.RaiseAndSetIfChanged(ref _carVin, value);
        }

        #endregion

        #region CarEngineNumberProperty

        private string _carEngineNumber;

        public string CarEngineNumber
        {
            get => _carEngineNumber;
            set => this.RaiseAndSetIfChanged(ref _carEngineNumber, value);
        }

        #endregion

        #region CarBodyNumberProperty

        private string _carBodyNumber;

        public string CarBodyNumber
        {
            get => _carBodyNumber;
            set => this.RaiseAndSetIfChanged(ref _carBodyNumber, value);
        }

        #endregion

        #region CarOwnerProperty

        private Client _owner;

        public Client CarOwner
        {
            get => _owner;
            set => this.RaiseAndSetIfChanged(ref _owner, value);
        }

        #endregion

        #endregion
    }
}