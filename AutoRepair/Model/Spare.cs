using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class Spare : ReactiveObject
    {
        #region Construcotrs

        public Spare() { }

        public Spare(string spareName, string spareWarranty, string sparePrice)
        {
            _spareName     = spareName;
            _spareWarranty = spareWarranty;
            _sparePrice    = sparePrice;
        }

        #endregion

        #region SpareIDProperty

        private int _spareId;
        [Key][Display(AutoGenerateField = false)]
        public int SpareId
        {
            get => _spareId;
            set => this.RaiseAndSetIfChanged(ref _spareId, value);
        }

        #endregion

        #region SpareNameProperty

        private string _spareName;
        [Display(Name = "Название")]
        public string SpareName
        {
            get => _spareName;
            set => this.RaiseAndSetIfChanged(ref _spareName, value);
        }

        #endregion

        #region SpareWarrantyProperty

        private string _spareWarranty;
        [Display(Name = "Гарантия")]
        public string SpareWarranty
        {
            get => _spareWarranty;
            set => this.RaiseAndSetIfChanged(ref _spareWarranty, value);
        }

        #endregion

        #region OrdersSparesProperty

        private List<OrdersSpares> _order;
        public List<OrdersSpares> OrdersSpares
        {
            get => _order;
            set => this.RaiseAndSetIfChanged(ref _order, value);
        }

        #endregion

        #region SaprePriceProperty

        private string _sparePrice;
        [Display(Name = "Цена")]
        public string SparePrice
        {
            get => _sparePrice;
            set => this.RaiseAndSetIfChanged(ref _sparePrice, value);
        }

        #endregion
    }
}