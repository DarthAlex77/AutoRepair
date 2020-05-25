using System.Collections.Generic;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class Order : ReactiveObject
    {
        #region Constructors

        public Order()
        {
        }

        public Order(Car car, Client client, List<Spare> sparesList)
        {
            _car = car;
            _client = client;
            _sparesList = sparesList;
        }

        #endregion

        #region OrderIdProperty

        private int _orderId;

        public int OrderId
        {
            get => _orderId;
            set => this.RaiseAndSetIfChanged(ref _orderId, value);
        }

        #endregion

        #region CarProperty

        private Car _car;

        public Car Car
        {
            get => _car;
            set => this.RaiseAndSetIfChanged(ref _car, value);
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

        #region OdrerServicesProperty

        private List<OrderServices> _orderServices;

        public List<OrderServices> OrderServices
        {
            get => _orderServices;
            set => this.RaiseAndSetIfChanged(ref _orderServices, value);
        }

        #endregion

        #region OderSparesProperty

        private List<Spare> _sparesList;

        public List<Spare> OderSpares
        {
            get => _sparesList;
            set => this.RaiseAndSetIfChanged(ref _sparesList, value);
        }

        #endregion
    }
}