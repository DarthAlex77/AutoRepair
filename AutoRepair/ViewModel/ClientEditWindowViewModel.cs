using System;
using System.Reactive;
using System.Reactive.Linq;
using AutoRepair.Model;
using AutoRepair.Validators;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.FluentValidation;

namespace AutoRepair.ViewModel
{
    public class ClientEditWindowViewModel : ReactiveValidationObject
    {
        #region Constructor

        public ClientEditWindowViewModel():base(new ClientValidator())
        {
            MessageBus.Current.Listen<int>   ("WindowMode").Subscribe(x => WindowState = x);
            MessageBus.Current.Listen<Client>("EditClient").Subscribe(x=>Client=x);
            using (AppContext db = new AppContext())
            {
                Clients = new ObservableCollectionExtended<Client>(db.Clients);
            }

            AddClientCommand    = ReactiveCommand.Create(AddClient,isValid);
            EditClientCommand   = ReactiveCommand.Create(EditClient,isValid);
            SelectClientCommand = ReactiveCommand.Create(SelectClient, IsClientSelected);
            Validation();
        }

        #endregion

        #region ClientsProperty

        public ObservableCollectionExtended<Client> Clients { get; set; }

        #endregion

        #region CloseWindowProperty

        private bool _closeTrigger;

        public bool CloseTrigger
        {
            get => _closeTrigger;
            set => this.RaiseAndSetIfChanged(ref _closeTrigger, value);
        }

        #endregion

        #region ClientProperty

        private Client _client;
        public Client Client
        {
            get => _client;
            set => this.RaiseAndSetIfChanged(ref _client, value);
        }
        #endregion

        #region WindowStateProperty

        private int _windowState;

        public int WindowState
        {
            get => _windowState;
            set => this.RaiseAndSetIfChanged(ref _windowState, value);
        }

        #endregion

        #region SelectedClientProperty

        private Client _selectedClient;

        public Client SelectedClient
        {
            get => _selectedClient;
            set => this.RaiseAndSetIfChanged(ref _selectedClient, value);
        }

        #endregion

        #region ValidationMethod

        private void Validation()
        {
            RaiseValidation((Client.Address));
            RaiseValidation(nameof(Client.PhoneNumber));
            RaiseValidation(nameof(Client.PersonalId));
            RaiseValidation(nameof(Client.Patronymic));
            RaiseValidation(nameof(Client.LastName));
            RaiseValidation(nameof(Client.FirstName));
        }

        #endregion

        #region AddClientCommand

        public ReactiveCommand<Unit, Unit> AddClientCommand { get; }

        private void AddClient()
        {
            using (AppContext db = new AppContext())
            {
                db.Clients.Add(new Client(Client.FirstName, Client.LastName, Client.Patronymic, Client.PersonalId, Client.PhoneNumber, Client.Address));
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion

        #region EditClientCommand

        public ReactiveCommand<Unit, Unit> EditClientCommand { get; }

        private void EditClient()
        {
            using (AppContext db = new AppContext())
            {
                Client client = db.Clients.Find(Client.ClientId);
                client.FirstName   = Client.FirstName;
                client.LastName    = Client.LastName;
                client.Patronymic  = Client.Patronymic;
                client.PersonalId  = Client.PersonalId;
                client.PhoneNumber = Client.PhoneNumber;
                client.Address     = Client.Address;
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion

        #region SelectClientCommand

        public  ReactiveCommand<Unit, Unit> SelectClientCommand { get; }
        private IObservable<bool>IsClientSelected=> this.WhenAnyValue(x => x.SelectedClient).Select(x => x != null);

        private void SelectClient()
        {
            MessageBus.Current.SendMessage(SelectedClient, "SelectedClient");
            CloseTrigger = true;
        }

        #endregion
    }
}