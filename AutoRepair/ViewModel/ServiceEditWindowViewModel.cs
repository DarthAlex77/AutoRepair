using System;
using System.Reactive;
using AutoRepair.Model;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    public class ServiceEditWindowViewModel :ReactiveObject
    {
        #region Constructor
        public ServiceEditWindowViewModel()
        {
            MessageBus.Current.Listen<bool>   ("ServiceEditWindowMode").Subscribe(x => WindowEditMode = x);
            MessageBus.Current.Listen<Service>("EditService").Subscribe(x => Service = x);
            AddServiceCommand  = ReactiveCommand.Create(AddService);
            EditServiceCommand = ReactiveCommand.Create(EditService);
            if (WindowEditMode)
            {
                MessageBus.Current.Listen<Service>("EditService").Subscribe(x => Service = x);
            }
            else
            {
                Service = new Service();
            }
        }
        #endregion

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

        #region ServiceProperty

        private Service _service;

        public Service Service
        {
            get => _service;
            set => this.RaiseAndSetIfChanged(ref _service, value);
        }

        #endregion

        #region AddServiceCommand

        public ReactiveCommand<Unit, Unit> AddServiceCommand { get; }

        private void AddService()
        {
            using (AppContext db = new AppContext())
            {
                db.Services.Add(new Service(Service.ServiceName, Service.ServicePrice, Service.WarrantyPeriod, Service.ServiceNote));
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion

        #region EditServiceCommand

        public ReactiveCommand<Unit, Unit> EditServiceCommand { get; }

        private void EditService()
        {
            using (AppContext db = new AppContext())
            {
                Service service        = db.Services.Find(Service.ServiceId);
                service.ServiceName    = Service.ServiceName;
                service.ServicePrice   = Service.ServicePrice;
                service.WarrantyPeriod = Service.WarrantyPeriod;
                service.ServiceNote    = Service.ServiceNote;
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion
    }
}