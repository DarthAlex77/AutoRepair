using System;
using System.Reactive;
using AutoRepair.Model;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class SpareEditWindowViewModel : ReactiveObject
    {
        public SpareEditWindowViewModel()
        {
            MessageBus.Current.Listen<bool>("SpareEditWindowMode").Subscribe(x => WindowEditMode = x);
            MessageBus.Current.Listen<Spare>("EditSpare").Subscribe(x => Spare = x);
            AddSpareCommand  = ReactiveCommand.Create(AddSpare);
            EditSpareCommand = ReactiveCommand.Create(EditSpare);
            if (WindowEditMode)
                MessageBus.Current.Listen<Spare>("EditSpare").Subscribe(x => Spare = x);
            else
                Spare = new Spare();
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

        #region SpareProperty

        private Spare _spare;

        public Spare Spare
        {
            get => _spare;
            set => this.RaiseAndSetIfChanged(ref _spare, value);
        }

        #endregion

        #region AddSpareCommand

        public ReactiveCommand<Unit, Unit> AddSpareCommand { get; }

        private void AddSpare()
        {
            using (AppContext db = new AppContext())
            {
                db.Spares.Add(new Spare(Spare.SpareName,Spare.SpareWarranty,Spare.SparePrice));
                db.SaveChanges();
            }
            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion

        #region EditSpareCommand

        public ReactiveCommand<Unit, Unit> EditSpareCommand { get; }

        private void EditSpare()
        {
            using (AppContext db = new AppContext())
            {
                Spare spare = db.Spares.Find(Spare.SpareId);
                spare.SparePrice = Spare.SparePrice;
                spare.SpareName = Spare.SpareName;
                spare.SpareWarranty = Spare.SpareWarranty;
                db.SaveChanges();
            }

            UpdateDatabaseEvent.OnDatabaseUpdated();
            CloseTrigger = true;
        }

        #endregion
    }
}