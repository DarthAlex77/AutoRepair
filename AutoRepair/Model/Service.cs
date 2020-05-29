using System.Collections.Generic;
using System.ComponentModel;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class Service : ReactiveObject
    {
        #region Constructors

        public Service()
        {
        }

        public Service(string serviceName, string servicesPrice, string warrantyPeriod)
        {
            _serviceName = serviceName;
            _servicesPrice = servicesPrice;
            _warrantyPeriod = warrantyPeriod;
        }

        #endregion

        #region ServiceIDProperty

        private int _serviceId;

        public int ServiceId
        {
            get => _serviceId;
            set => this.RaiseAndSetIfChanged(ref _serviceId, value);
        }

        #endregion

        #region ServiceNameProperty

        private string _serviceName;

        public string ServiceName
        {
            get => _serviceName;
            set => this.RaiseAndSetIfChanged(ref _serviceName, value);
        }

        #endregion

        #region ServicePriceProperty

        private string _servicesPrice;

        public string ServicePrice
        {
            get => _servicesPrice;
            set => this.RaiseAndSetIfChanged(ref _servicesPrice, value);
        }

        #endregion

        #region WarrantyPeriodProperty

        private string _warrantyPeriod;

        public string WarrantyPeriod
        {
            get => _warrantyPeriod;
            set => this.RaiseAndSetIfChanged(ref _warrantyPeriod, value);
        }

        #endregion

    }
}