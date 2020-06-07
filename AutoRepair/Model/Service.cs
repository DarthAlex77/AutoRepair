using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class Service : ReactiveObject
    {
        #region Constructors

        public Service()
        {
        }

        public Service(string serviceName, string servicesPrice, string warrantyPeriod, string serviceNote)
        {
            _serviceName = serviceName;
            _servicesPrice = servicesPrice;
            _warrantyPeriod = warrantyPeriod;
            _serviceNote = serviceNote;
        }

        #endregion

        #region ServiceIDProperty

        private int _serviceId;
        [Display(AutoGenerateField = false)]
        public int ServiceId
        {
            get => _serviceId;
            set => this.RaiseAndSetIfChanged(ref _serviceId, value);
        }

        #endregion

        #region ServiceNameProperty

        private string _serviceName;
        [Display(Name = "Название")]
        public string ServiceName
        {
            get => _serviceName;
            set => this.RaiseAndSetIfChanged(ref _serviceName, value);
        }

        #endregion

        #region ServicePriceProperty

        private string _servicesPrice;
        [Display(Name = "Цена")]
        public string ServicePrice
        {
            get => _servicesPrice;
            set => this.RaiseAndSetIfChanged(ref _servicesPrice, value);
        }

        #endregion

        #region WarrantyPeriodProperty

        private string _warrantyPeriod;
        [Display(Name = "Гарантия")]
        public string WarrantyPeriod
        {
            get => _warrantyPeriod;
            set => this.RaiseAndSetIfChanged(ref _warrantyPeriod, value);
        }

        #endregion

        #region ServiceNoteProperty

        private string _serviceNote;
        [Display(Name = "Примечание")]
        public string ServiceNote
        {
            get => _serviceNote;
            set => this.RaiseAndSetIfChanged(ref _serviceNote, value);
        }

        #endregion

        #region OrderServicesProperty

        private List<OrdersServices> _orderServices;

        public List<OrdersServices> OrderServices
        {
            get => _orderServices;
            set => this.RaiseAndSetIfChanged(ref _orderServices, value);
        }

        #endregion
    }
}