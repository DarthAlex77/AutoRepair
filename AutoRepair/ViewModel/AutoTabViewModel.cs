using AutoRepair.Model;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class AutoTabViewModel : ReactiveObject
    {
        #region Constructor

        public AutoTabViewModel()
        {
            using (var db = new AppContext())
            {
                Cars = new ObservableCollectionExtended<Car>(db.Cars);
                db.CarModels.Load();
                db.Clients.Load();
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