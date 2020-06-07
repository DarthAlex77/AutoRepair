using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using AutoRepair.Behaviors;
using AutoRepair.Model;
using AutoRepair.View;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class CarsTabViewModel :ReactiveObject
    {
        #region Constructor

        public CarsTabViewModel()
        {
            EditCarCommand                      =  ReactiveCommand.Create(EditCar, IsCarSelected);
            DeleteCarCommand                    =  ReactiveCommand.Create(DeleteCar, IsCarSelected);
            AddCarCommand                       =  ReactiveCommand.Create(AddCar);
            UpdateDatabaseEvent.DatabaseUpdated += DataBaseUpdated;
            Cars                                =  new ObservableCollectionExtended<Car>();
            DataBaseUpdated();
        }

        #endregion

        #region IsCarSelectedProperty

        private IObservable<bool> IsCarSelected => this.WhenAnyValue(x => x.SelectedCar).Select(x => x != null);

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

        #region DataBaseUpdatedMethod

        private void DataBaseUpdated()
        {
            using (AppContext db = new AppContext())
            {
                Cars.Load(db.Cars.Include(x => x.CarOwner).Include(x => x.CarModel));
            }
        }

        #endregion

        #region EditCarCommand

        public ReactiveCommand<Unit, Unit> EditCarCommand { get; }

        private void EditCar()
        {
            new CarEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(SelectedCar.CarId, "EditCarId");
        }

        #endregion

        #region AddCarCommand

        public ReactiveCommand<Unit, Unit> AddCarCommand { get; }

        private void AddCar()
        {
            new CarEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(true, "AddMode");
        }

        #endregion

        #region DeleteCarCommand

        public ReactiveCommand<Unit, Unit> DeleteCarCommand { get; }

        private void DeleteCar()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удалить машину" + Environment.NewLine +
                                                                "Внимание удаление машины приведет к удалению всех заказов на эту машину", "Удалить?",
                    MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                using (AppContext db = new AppContext())
                {
                    Car car = db.Cars.Find(SelectedCar.CarId);
                    db.Cars.Remove(car);
                    db.SaveChanges();
                }

                UpdateDatabaseEvent.OnDatabaseUpdated();
            }
        }

        #endregion
    }
}