using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using AutoRepair.Model;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    class ItemSelectorWindowViewModel:ReactiveObject
    {

        #region Constructor
        public ItemSelectorWindowViewModel()
        {
            SelectedItems=new ObservableCollectionExtended<object>();
            SelectCarCommand = ReactiveCommand.Create(SelectCar);
            SelectClientCommand = ReactiveCommand.Create(SelectClient);
            SelectServicesCommand = ReactiveCommand.Create(SelectServices);
            SelectSparesCommand = ReactiveCommand.Create(SelectSpares);
            MessageBus.Current.Listen<int>("SelectorWindowMode").Subscribe(OnNext);
        }

        private void OnNext(int obj)
        {
            WindowMode = obj;
            Initialize();
        }

        private void Initialize()
        {
            using (AppContext db = new AppContext())
            {
                switch (WindowMode)
                {
                    case 1:
                        Cars = new ObservableCollectionExtended<Car>(db.Cars.Include(x => x.CarModel));
                        break;
                    case 2:
                        Clients = new ObservableCollectionExtended<Client>(db.Clients);
                        break;
                    case 3:
                        Services = new ObservableCollectionExtended<Service>(db.Services);
                        break;
                    case 4:
                        Spares = new ObservableCollectionExtended<Spare>(db.Spares);
                        break;
                    default:
                        throw new InvalidDataException("Значение должно быть от 1 до 4");
                }
            }
        }

        #endregion
       
        #region WindowModeProperty

        private int _windowsMode;

        public int WindowMode
        {
            get => _windowsMode;
            set => this.RaiseAndSetIfChanged(ref _windowsMode, value);
        }

        #endregion

        #region IsItemSelected

        private IObservable<bool> IsItemSelected => this.WhenAnyValue(x => x.SelectedItem).Select(x => x != null);

        #endregion

        #region CloseTriggerProperty

        private bool _closeTrigger;

        public bool CloseTrigger
        {
            get => _closeTrigger;
            set => this.RaiseAndSetIfChanged(ref _closeTrigger, value);
        }

        #endregion

        #region SelectedItemProperty

        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        #endregion

        #region SelectedItemsProperty

        private ObservableCollection<object> _selectedItems; 
        public ObservableCollection<object> SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                RaisePropertyChanged("SelectedItems");
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        #region SelectionProperties

        #region CarsProperty

        private ObservableCollectionExtended<Car> _cars;
        public ObservableCollectionExtended<Car> Cars
        {
            get => _cars;
            set => this.RaiseAndSetIfChanged(ref _cars, value);
        }

        #endregion

        #region ClientsProperty

        private ObservableCollectionExtended<Client> _clients;
        public ObservableCollectionExtended<Client> Clients
        {
            get => _clients;
            set => this.RaiseAndSetIfChanged(ref _clients, value);
        }

        #endregion
        #region ServicesProperty

        private ObservableCollectionExtended<Service> _services;
        public ObservableCollectionExtended<Service> Services
        {
            get => _services;
            set => this.RaiseAndSetIfChanged(ref _services, value);
        }

        #endregion
        #region SparesProperty

        private ObservableCollectionExtended<Spare> _spares;
        public ObservableCollectionExtended<Spare> Spares
        {
            get => _spares;
            set => this.RaiseAndSetIfChanged(ref _spares, value);
        }

        #endregion

        #endregion

        #region SelectCarCommand

        public ReactiveCommand<Unit, Unit> SelectCarCommand { get; }

        private void SelectCar()
        {
            Car car = SelectedItem as Car;
            MessageBus.Current.SendMessage(car,"SelectedCar");
            CloseTrigger = true;
        }

        #endregion

        #region SelectClientCommand

        public ReactiveCommand<Unit, Unit> SelectClientCommand { get;}

        private void SelectClient()
        {
            Client client=SelectedItem as Client;
            MessageBus.Current.SendMessage(client,"SelectedClient");
            CloseTrigger = true;
        }

        #endregion

        #region SelectSparesCommand

        public ReactiveCommand<Unit, Unit> SelectSparesCommand { get; }

        private void SelectSpares()
        {
            List<Spare> spares = SelectedItems.Cast<Spare>().ToList();
            MessageBus.Current.SendMessage(spares,"SelectedSpares");
            CloseTrigger = true;
        }

        #endregion

        #region SelectServicesCommand

        public ReactiveCommand<Unit, Unit> SelectServicesCommand { get; }

        private void SelectServices()
        {
            List<Service> services=new List<Service>();
            services = SelectedItems.Cast<Service>().ToList();
            MessageBus.Current.SendMessage(services,"SelectedServices");
            CloseTrigger = true;
        }

        #endregion
    }
    
}
