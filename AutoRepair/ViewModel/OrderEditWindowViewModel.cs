using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using AutoRepair.Behaviors;
using AutoRepair.Model;
using AutoRepair.View;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.FluentValidation;

namespace AutoRepair.ViewModel
{
   public class OrderEditWindowViewModel:ReactiveObject
    {
        public OrderEditWindowViewModel()
        {
            AddOrderCommand = ReactiveCommand.Create(AddOrder);
            EditOrderCommand = ReactiveCommand.Create(EditOrder);
            SelectClientCommand = ReactiveCommand.Create(SelectClient);
            SelectCarCommand = ReactiveCommand.Create(SelectCar);
            SelectServicesCommand = ReactiveCommand.Create(SelectServices);
            SelectSparesCommand = ReactiveCommand.Create(SelectSpares);
            MessageBus.Current.Listen<bool>("OrderEditWindowMode").Subscribe(x=>WindowEditMode = x);
            MessageBus.Current.Listen<int>("OrderEdit").Subscribe(LoadOrder);
        }

        private void LoadOrder(int orderId)
        {
            using (AppContext db=new AppContext())
            {
                Order = db.Orders.Include(x => x.Client)
                    .Include(x => x.OrderServices).ThenInclude(x=>x.Service)
                    .Include(x => x.OrdersSpares).ThenInclude(x=>x.Spare)
                    .Include(x => x.Car).ThenInclude(x => x.CarModel)
                    .First(x => x.OrderId == orderId);
            }
        }

        #region WindowModeProperty

        private bool _windowsEditMode;

        public bool WindowEditMode
        {
            get => _windowsEditMode;
            set => this.RaiseAndSetIfChanged(ref _windowsEditMode, value);
        }

        #endregion

        #region CloseTriggerProperty

        private bool _closeTrigger;

        public bool CloseTrigger
        {
            get => _closeTrigger;
            set => this.RaiseAndSetIfChanged(ref _closeTrigger, value);
        }

        #endregion

        #region AddOrderCommand

        public ReactiveCommand<Unit, Unit> AddOrderCommand { get; }

        private void AddOrder()
        {

        }

        #endregion

        #region EditorderCommand

        public ReactiveCommand<Unit, Unit> EditOrderCommand { get; }

        private void EditOrder()
        {

        }

        #endregion

        #region OrderProperty

        private Order _order;
        public Order Order
        {
            get => _order;
            set => this.RaiseAndSetIfChanged(ref _order, value);
        }

        #endregion

        #region SelectCarCommand

        public ReactiveCommand<Unit, Unit> SelectCarCommand { get; }

        private void SelectCar()
        {
            new ItemSelectorWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(1,"SelectorWindowMode");
            MessageBus.Current.Listen<Car>("SelectedCar").Subscribe(CarSelected);
        }

        private void CarSelected(Car car)
        {
            using (AppContext db=new AppContext())
            {
                db.CarModels.Load();
                Order.Car = db.Cars.Find(car.CarId);
            }
        }

        #endregion

        #region SelectClientCommand

        public ReactiveCommand<Unit, Unit> SelectClientCommand { get; }

        private void SelectClient()
        {
            new ItemSelectorWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(2,"SelectorWindowMode");
            MessageBus.Current.Listen<Client>("SelectedClient").Subscribe(ClientSelected);
        }
        private void ClientSelected(Client client)
        {
            using (AppContext db=new AppContext())
            {
                Order.Client = db.Clients.Find(client.ClientId);
            }
        }

        #endregion

        #region SelectServicesCommand

        public ReactiveCommand<Unit, Unit> SelectServicesCommand { get; }

        private void SelectServices()
        {
            new ItemSelectorWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(3,"SelectorWindowMode");
            MessageBus.Current.Listen<List<Service>>("SelectedServices").Subscribe(ServicesSelected);
        }
        private void ServicesSelected(List<Service>services)
        {
            using (AppContext db=new AppContext())
            {

            }
        }

        #endregion

        #region SelectSparesCommand

        public ReactiveCommand<Unit, Unit> SelectSparesCommand { get; }

        private void SelectSpares()
        {
            new ItemSelectorWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(4,"SelectorWindowMode");
            MessageBus.Current.Listen<List<Spare>>("SelectedSpares").Subscribe(SparesSelected);
        }
        private void SparesSelected(List<Spare> spares)
        {
            using (AppContext db=new AppContext())
            {

            }
        }

        #endregion
    }
}