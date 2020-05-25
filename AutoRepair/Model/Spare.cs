using System.Collections.Generic;
using System.ComponentModel;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class Spare : ReactiveObject
    {
        #region Construcotrs
        public Spare()
        {
        }

        public Spare(string spareName, string spareWarranty, string sparePrice)
        {
            _spareName = spareName;
            _spareWarranty = spareWarranty;
            _sparePrice = sparePrice;
        }

        #endregion

        #region SpareIDProperty

        private string _spareId;
        public string SpareId
        {
            get => _spareId;
            set => this.RaiseAndSetIfChanged(ref _spareId, value);
        }

        #endregion

        #region SpareNameProperty

        private string _spareName;

        public string SpareName
        {
            get => _spareName;
            set => this.RaiseAndSetIfChanged(ref _spareName, value);
        }

        #endregion

        #region SpareWarrantyProperty

        private string _spareWarranty;

        public string SpareWarranty
        {
            get => _spareWarranty;
            set => this.RaiseAndSetIfChanged(ref _spareWarranty, value);
        }

        #endregion

        #region SaprePriceProperty

        private string _sparePrice;

        public string SparePrice
        {
            get => _sparePrice;
            set => this.RaiseAndSetIfChanged(ref _sparePrice, value);
        }

        #endregion
    }
}