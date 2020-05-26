using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Forms;
using AutoRepair.Model;
using AutoRepair.View;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using MessageBox = System.Windows.MessageBox;

namespace AutoRepair.ViewModel
{
    internal class AutoTabViewModel : ReactiveObject
    {
        #region Constructor

        public AutoTabViewModel()
        {
            EditCarCommand = ReactiveCommand.Create(EditCar, IsCarSelected);
            DeleteCarCommand = ReactiveCommand.Create(DeleteCar, IsCarSelected);
            AddCarCommand = ReactiveCommand.Create(AddCar);

            using (AppContext db = new AppContext())
            {
                Cars = new ObservableCollectionExtended<Car>(db.Cars);
                db.CarModels.Load();
                db.Clients.Load();
            }
        }

        #endregion

        #region IsCarSelectedProperty
        private IObservable<bool> IsCarSelected => this.WhenAnyValue(x => x.SelectedCar).Select(x => x != null);
        #endregion

        #region EditCarCommand

        public ReactiveCommand<Unit, Unit> EditCarCommand { get; }

        private void EditCar()
        {
            new AutoEditWindow().Show();
            MessageBus.Current.SendMessage(SelectedCar.CarId,"EditCarId");
        }


        #endregion

        #region AddCarCommand

        public ReactiveCommand<Unit, Unit> AddCarCommand { get; }

        private void AddCar()
        {
            new AutoEditWindow().Show();
            MessageBus.Current.SendMessage(true,"AddMode");
        }

        #endregion

        #region DeleteCarCommand

        public ReactiveCommand<Unit, Unit> DeleteCarCommand { get; }

        private void DeleteCar()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удалить машину", "Удалить?",
                            MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                using (AppContext db = new AppContext())
                {
                    Car car = db.Cars.Find(SelectedCar.CarId);
                    db.Cars.Remove(car);
                    db.SaveChanges();
                    Cars.Remove(car);
                }
            }
        }

        #endregion

        #region CarsProperty

        public ObservableCollectionExtended<Car> Cars { get; set; }

        #endregion

        #region SelectedCarProperty

        private Car _car;

        public Car SelectedCar
        {
            get => _car;
            set => this.RaiseAndSetIfChanged(ref _car, value);
        }

        #endregion
    }
}