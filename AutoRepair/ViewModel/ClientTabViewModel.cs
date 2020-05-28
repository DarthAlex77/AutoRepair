using System;
using System.Collections.ObjectModel;
using System.Linq;
using AutoRepair.Model;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using AutoRepair.View;

namespace AutoRepair.ViewModel
{
    internal class ClientTabViewModel : ReactiveObject
    {
        #region Constructors

        public ClientTabViewModel()
        {
            AddClientCommand = ReactiveCommand.Create(AddClient);
            EditClientCommand = ReactiveCommand.Create(EditClient, IsClientSelected);
            DeleteClientCommand = ReactiveCommand.Create(DeleteClient, IsClientSelected);
            UpdateDatabaseEvent.DatabaseUpdated += DataBaseUpdated;
            Clients=new ObservableCollectionExtended<Client>();
            DataBaseUpdated();
        }

        private void DataBaseUpdated()
        {
            using (AppContext db=new AppContext())
            {
                Clients.Load(db.Clients.Include(x=>x.ClientCars).ThenInclude(x=>x.CarModel));
            }
        }


        #endregion

        #region IsClientSelectedProperty
        private IObservable<bool> IsClientSelected => this.WhenAnyValue(x => x.SelectedClient).Select(x => x != null);
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

        #region AddClientCommand
        public ReactiveCommand<Unit, Unit> AddClientCommand { get; }
        private void AddClient()
        {
            new ClientEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(0,"WindowMode");
        }
        #endregion

        #region EditClientCommand
        public ReactiveCommand<Unit, Unit> EditClientCommand { get; }
        private void EditClient()
        {
            new ClientEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(1,"WindowMode");
            MessageBus.Current.SendMessage(SelectedClient,"EditClient");
        }
        #endregion

        #region DeleteClientCommand
        public ReactiveCommand<Unit, Unit> DeleteClientCommand { get; }
        private void DeleteClient()
        {
            using (AppContext db=new AppContext())
            {
                Client client = db.Clients.Include(x=>x.ClientCars).First(x => x.ClientID == SelectedClient.ClientID);

                db.Clients.Remove(client);
                db.SaveChanges();
            }
            DataBaseUpdated();
        }
        #endregion

    }
}