using AutoRepair.Model;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class ClientTabViewModel : ReactiveObject
    {
        #region Constructors

        public ClientTabViewModel()
        {
            using (AppContext db = new AppContext())
            {
                Clients = new ObservableCollectionExtended<Client>(db.Clients);
                db.Cars.Load();
                db.CarModels.Load();
            }
        }

        #endregion

        #region ClientsProperty

        public ObservableCollectionExtended<Client> Clients { get; set; }

        #endregion

        #region SelectedClientProperty

        private Client _selectedClient;

        public Client SelectedClient
        {
            get => _selectedClient;
            set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
        }

        #endregion
    }
}