using System;
using AutoRepair.Model;
using DynamicData.Binding;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using AutoRepair.Behaviors;
using AutoRepair.View;

namespace AutoRepair.ViewModel
{
    public class ServicesTabViewModel:ReactiveObject
    {
        #region Constructor
        public ServicesTabViewModel()
        {
            AddServiceCommand                   =  ReactiveCommand.Create(AddService);
            EditServiceCommand                  =  ReactiveCommand.Create(EditService,IsServiceSelected);
            DeleteServiceCommand                =  ReactiveCommand.Create(DeleteService,IsServiceSelected);
            Services                            =  new ObservableCollectionExtended<Service>();
            UpdateDatabaseEvent.DatabaseUpdated += DataBaseUpdated;
            DataBaseUpdated();
        }

        #endregion

        #region DataBaseUpdatedMethod

        private void DataBaseUpdated()
        {
            using (AppContext db = new AppContext())
            {
                Services.Load(db.Services);
            }
        }

        #endregion

        #region IsServiceSelectedProperty

        private IObservable<bool> IsServiceSelected => this.WhenAnyValue(x => x.SelectedService).Select(x => x != null);

        #endregion

        #region ServicesProperty
        public ObservableCollectionExtended<Service> Services { get; set; }

        #endregion

        #region SelectedServiceProperty

        private Service _selectedService;
        public Service SelectedService
        {
            get => _selectedService;
            set => this.RaiseAndSetIfChanged(ref _selectedService, value);
        }
        #endregion

        #region AddServiceCommand
        public ReactiveCommand<Unit, Unit> AddServiceCommand { get; }
        private void AddService()
        {
            new ServiceEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(false,"ServiceEditWindowMode");
        }
        #endregion

        #region EditServiceCommand
        public ReactiveCommand<Unit, Unit> EditServiceCommand { get; }
        private void EditService()
        {
            new ServiceEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(true,"ServiceEditWindowMode");
            MessageBus.Current.SendMessage(SelectedService,"EditService");
        }
        #endregion

        #region DeleteServiceCommand
        public ReactiveCommand<Unit, Unit> DeleteServiceCommand { get; }
        private void DeleteService()
        {
            using (AppContext db=new AppContext())
            {
              Service service=  db.Services.Find(SelectedService.ServiceId);
              db.Services.Remove(service);
              db.SaveChanges();
            }
            UpdateDatabaseEvent.OnDatabaseUpdated();
        }
        #endregion

    }
}
