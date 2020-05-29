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

        public Order(Car car, Client client)
        {
            _car = car;
            _client = client;
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
    }
}