using System;
using System.Reactive;
using AutoRepair.Model;
using DynamicData.Binding;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class ClientEditWindowViewModel : ReactiveObject
    {
        #region Constructor

        public ClientEditWindowViewModel()
        {
            MessageBus.Current.Listen<int>("WindowMode").Subscribe(x => WindowState = x);
            using (AppContext db = new AppContext())
            {
                Clients = new ObservableCollectionExtended<Client>(db.Clients);
            }

            AddClientCommand = ReactiveCommand.Create(AddClient);
            EditClientCommand = ReactiveCommand.Create(EditClient);
            SelectClientCommand = ReactiveCommand.Create(SelectClient);
        }

        #endregion

        #region CloseWindowProperty

        private bool _closeWindow;
        public bool CloseWindow
        {
            get => _closeWindow;
            set => this.RaiseAndSetIfChanged(ref _closeWindow, value);
        }
        #endregion

        #region ClientsProperty

        public ObservableCollectionExtended<Client> Clients { get; set; }

        #endregion

        #region AddClientCommand

        public ReactiveCommand<Unit, Unit> AddClientCommand { get; }

        private void AddClient()
        {
        }

        #endregion

        #region EditClientCommand

        public ReactiveCommand<Unit, Unit> EditClientCommand { get; }

        private void EditClient()
        {
        }

        #endregion

        #region SelectClientCommand

        public ReactiveCommand<Unit, Unit> SelectClientCommand { get; }

        private void SelectClient()
        {
            MessageBus.Current.SendMessage(SelectedClient,"SelectedClient");
            CloseWindow = true;
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

        #region WindowStateProperty

        private int _windowState;

        public int WindowState
        {
            get => _windowState;
            set => this.RaiseAndSetIfChanged(ref _windowState, value);
        }

        #endregion

        #region ClientProperties

        #region FirstNameProperty

        private string _firstname;

        public string FirstName
        {
            get => _firstname;
            set => this.RaiseAndSetIfChanged(ref _firstname, value);
        }

        #endregion

        #region LastNameProperty

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set => this.RaiseAndSetIfChanged(ref _lastName, value);
        }

        #endregion

        #region PatronymicProperty

        private string _patronymic;

        public string Patronymic
        {
            get => _patronymic;
            set => this.RaiseAndSetIfChanged(ref _patronymic, value);
        }

        #endregion

        #region PersonalIDProperty

        private string _personalId;

        public string PersonalId
        {
            get => _personalId;
            set => this.RaiseAndSetIfChanged(ref _personalId, value);
        }

        #endregion

        #region PhoneNumberProperty

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
        }

        #endregion

        #region AddressProperty

        private string _address;

        public string Address
        {
            get => _address;
            set => this.RaiseAndSetIfChanged(ref _address, value);
        }

        #endregion

        #endregion
    }
}