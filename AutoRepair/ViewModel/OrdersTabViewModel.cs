using System;
using System.Reactive;
using System.Reactive.Linq;
using AutoRepair.Behaviors;
using AutoRepair.Model;
using AutoRepair.View;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    class OrdersTabViewModel:ReactiveObject
    {
        #region Constructor
        public OrdersTabViewModel()
        {
            AddOrderCommand = ReactiveCommand.Create(AddOrder);
            EditOrderCommand = ReactiveCommand.Create(EditOrder,IsOdredSelected);
            DeleteOrderCommand = ReactiveCommand.Create(DeleteOrder,IsOdredSelected);
            Orders=new ObservableCollectionExtended<Order>();
            UpdateDatabaseEvent.DatabaseUpdated += DataBaseUpdated;
            DataBaseUpdated();
        }

        #endregion

        #region IsClientSelectedProperty

        private IObservable<bool> IsOdredSelected => this.WhenAnyValue(x => x.SelectedOrder).Select(x => x != null);

        #endregion

        #region OrdersProperty

        public ObservableCollectionExtended<Order>Orders { get; set; }

        #endregion

        #region SelectedOrderProperty

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => this.RaiseAndSetIfChanged(ref _selectedOrder, value);
        }

        #endregion

        #region DataBaseUpdatedMethod

        private void DataBaseUpdated()
        {
            using (AppContext db=new AppContext())
            {
                Orders.Load(db.Orders.Include(x => x.Car).ThenInclude(x => x.CarModel)
                    .Include(x => x.Client)
                    .Include(x => x.OrderServices).ThenInclude(x => x.Service));
            }
        }

        #endregion

        #region AddClientCommand

        public ReactiveCommand<Unit, Unit> AddOrderCommand { get; }

        private void AddOrder()
        {
            new OrderEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(0, "OrderEditWindowMode");
        }

        #endregion

        #region EditOrderCommand

        public ReactiveCommand<Unit, Unit> EditOrderCommand { get; }

        private void EditOrder()
        {
            new OrderEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(true, "OrderEditWindowMode");
            MessageBus.Current.SendMessage(SelectedOrder.OrderId,"OrderEdit");
        }

        #endregion

        #region DeleteOrderCommand

        public ReactiveCommand<Unit, Unit> DeleteOrderCommand { get; }

        private void DeleteOrder()
        {
            using (AppContext db=new AppContext())
            {
                db.Orders.Remove(db.Orders.Find(SelectedOrder.OrderId));
                db.SaveChanges();
            }
            UpdateDatabaseEvent.OnDatabaseUpdated();
        }

        #endregion
    }
}
