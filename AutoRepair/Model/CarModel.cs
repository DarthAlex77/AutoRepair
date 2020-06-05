using System.ComponentModel.DataAnnotations;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class CarModel : ReactiveObject
    {
        #region Construcotrs

        public CarModel() { }

        public CarModel(string manufacturer, string model)
        {
            _manufacturer = manufacturer;
            _model        = model;
        }

        #endregion

        #region CarModelIDProperty

        private int _carModelId;

        public int CarModelId
        {
            get => _carModelId;
            set => this.RaiseAndSetIfChanged(ref _carModelId, value);
        }

        #endregion

        #region ManufacturerProperty

        private string _manufacturer;

        [Required]
        public string Manufacturer
        {
            get => _manufacturer;
            set => this.RaiseAndSetIfChanged(ref _manufacturer, value);
        }

        #endregion

        #region ModelProperty

        private string _model;

        [Required]
        public string Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }

        #endregion
    }
}