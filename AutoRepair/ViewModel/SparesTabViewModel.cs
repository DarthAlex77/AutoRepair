using System;
using System.Reactive;
using System.Reactive.Linq;
using AutoRepair.Behaviors;
using AutoRepair.Model;
using AutoRepair.View;
using DynamicData.Binding;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class SparesTabViewModel : ReactiveObject
    {
        public SparesTabViewModel()
        {
            AddSpareCommand                     =  ReactiveCommand.Create(AddSpare);
            EditSpareCommand                    =  ReactiveCommand.Create(EditSpare, IsSpareSelected);
            DeleteSpareCommand                  =  ReactiveCommand.Create(DeleteSpare, IsSpareSelected);
            Spares                              =  new ObservableCollectionExtended<Spare>();
            UpdateDatabaseEvent.DatabaseUpdated += DataBaseUpdated;
            DataBaseUpdated();
        }

        public ObservableCollectionExtended<Spare> Spares { get; set; }

        #region IsSpareSelectedProperty

        private IObservable<bool> IsSpareSelected => this.WhenAnyValue(x => x.SelectedSpare).Select(x => x != null);

        #endregion

        private void DataBaseUpdated()
        {
            using (AppContext db = new AppContext())
            {
                Spares.Load(db.Spares);
            }
        }

        #region SelectedSpareProperty

        private Spare _selectedSpare;

        public Spare SelectedSpare
        {
            get => _selectedSpare;
            set => this.RaiseAndSetIfChanged(ref _selectedSpare, value);
        }

        #endregion

        #region AddSpareCommand

        public ReactiveCommand<Unit, Unit> AddSpareCommand { get; }

        private void AddSpare()
        {
            new SpareEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(false, "SpareEditWindowMode");
        }

        #endregion

        #region EditSpareCommand

        public ReactiveCommand<Unit, Unit> EditSpareCommand { get; }

        private void EditSpare()
        {
            new SpareEditWindow().ShowDialogAsync();
            MessageBus.Current.SendMessage(true, "SpareEditWindowMode");
            MessageBus.Current.SendMessage(SelectedSpare, "EditSpare");
        }

        #endregion

        #region DeleteSpareCommand

        public ReactiveCommand<Unit, Unit> DeleteSpareCommand { get; }

        private void DeleteSpare()
        {
            using (AppContext db = new AppContext())
            {
                Spare spare = db.Spares.Find(SelectedSpare.SpareId);
                db.Spares.Remove(spare);
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
        }

        #endregion
    }
}