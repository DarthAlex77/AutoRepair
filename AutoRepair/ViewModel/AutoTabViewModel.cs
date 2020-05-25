using AutoRepair.Model;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Reactive;

namespace AutoRepair.ViewModel
{
    internal class AutoTabViewModel : ReactiveObject
    {
        #region Constructor
        public AutoTabViewModel()
        {
            EditCarCommand = ReactiveCommand.Create(EditCar);

            using (AppContext db = new AppContext())
            {
                Cars = new ObservableCollectionExtended<Car>(db.Cars);
                db.CarModels.Load();
                db.Clients.Load();
            }
        }
        #endregion

        #region EditCarCommand
        public ReactiveCommand<Unit, Unit> EditCarCommand { get; }
        private void EditCar()
        {
            new AutoEditWindowsViewModel(false);
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