﻿using System;
using System.Reactive;
using System.Reactive.Linq;
using AutoRepair.Behaviors;
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
            MessageBus.Current.Listen<Client>("EditClient").Subscribe(ClientSelected);
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

        #region ClientProperties

        #region ClientIDProperty

        private int _clientId;

        public int ClientId
        {
            get => _clientId;
            set => this.RaiseAndSetIfChanged(ref _clientId, value);
        }

        #endregion

        #region FirstNameProperty

        private string _firstname;

        public string FirstName
        {
            get => _firstname;
            set
            {
                this.RaiseAndSetIfChanged(ref _firstname, value);
                Validation();
            }
        }

        #endregion

        #region LastNameProperty

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                this.RaiseAndSetIfChanged(ref _lastName, value);
                Validation();
            }
        }

        #endregion

        #region PatronymicProperty

        private string _patronymic;

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                this.RaiseAndSetIfChanged(ref _patronymic, value);
                Validation();
            }
        }

        #endregion

        #region PersonalIDProperty

        private string _personalId;

        public string PersonalId
        {
            get => _personalId;
            set
            {
                this.RaiseAndSetIfChanged(ref _personalId, value);
                Validation();
            }
        }

        #endregion

        #region PhoneNumberProperty

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                this.RaiseAndSetIfChanged(ref _phoneNumber, value);
                Validation();
            }
        }

        #endregion

        #region AddressProperty

        private string _address;

        public string Address
        {
            get => _address;
            set
            {
                this.RaiseAndSetIfChanged(ref _address, value);
                Validation();
            }
        }

        #endregion

        #region RaiseValidationMethod

        private void Validation()
        {
            RaiseValidation(nameof(Address));
            RaiseValidation(nameof(PhoneNumber));
            RaiseValidation(nameof(PersonalId));
            RaiseValidation(nameof(Patronymic));
            RaiseValidation(nameof(LastName));
            RaiseValidation(nameof(FirstName));
        }

        #endregion

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

        #region AddClientCommand

        public ReactiveCommand<Unit, Unit> AddClientCommand { get; }

        private void AddClient()
        {
            using (AppContext db = new AppContext())
            {
                db.Clients.Add(new Client(FirstName, LastName, Patronymic, PersonalId, PhoneNumber, Address));
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion

        #region EditClientCommand

        public ReactiveCommand<Unit, Unit> EditClientCommand { get; }

        private void ClientSelected(Client obj)
        {
            ClientId = obj.ClientId;
            FirstName = obj.FirstName;
            LastName = obj.LastName;
            Patronymic = obj.Patronymic;
            PersonalId = obj.PersonalId;
            PhoneNumber = obj.PhoneNumber;
            Address = obj.Address;
        }

        private void EditClient()
        {
            using (AppContext db = new AppContext())
            {
                Client client = db.Clients.Find(ClientId);
                client.FirstName   = FirstName;
                client.LastName    = LastName;
                client.Patronymic  = Patronymic;
                client.PersonalId  = PersonalId;
                client.PhoneNumber = PhoneNumber;
                client.Address     = Address;
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