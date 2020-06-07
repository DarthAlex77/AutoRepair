using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class Order : ReactiveObject
    {
        #region Constructors

        public Order() { }

        public Order(Car car, Client client)
        {
            _car    = car;
            _client = client;
        }

        #endregion

        #region OrderIdProperty

        private int _orderId;
        [Display(Name = "№ Заказа")]
        public int OrderId
        {
            get => _orderId;
            set => this.RaiseAndSetIfChanged(ref _orderId, value);
        }

        #endregion

        #region CarProperty

        private Car _car;
        [Display(Name = "Машина")]
        public Car Car
        {
            get => _car;
            set => this.RaiseAndSetIfChanged(ref _car, value);
        }

        #endregion

        #region ClientProperty

        private Client _client;
        [Display(Name = "Клиент")]
        public Client Client
        {
            get => _client;
            set => this.RaiseAndSetIfChanged(ref _client, value);
        }

        #endregion

        #region OrderServiceProperty

        private List<OrdersServices> _orderServices;
        public List<OrdersServices> OrderServices
        {
            get => _orderServices;
            set => this.RaiseAndSetIfChanged(ref _orderServices, value);
        }

        #endregion

        #region OrderSparesProperty

        private List<OrdersSpares> _orderSpares;
        public List<OrdersSpares> OrdersSpares
        {
            get => _orderSpares;
            set => this.RaiseAndSetIfChanged(ref _orderSpares, value);
        }

        #endregion

        #region OrderNotesProperty

        private string _orderNotes;
        [Display(Name = "Примечание")]
        public string OrderNotes
        {
            get => _orderNotes;
            set => this.RaiseAndSetIfChanged(ref _orderNotes, value);
        }

        #endregion

    }
}