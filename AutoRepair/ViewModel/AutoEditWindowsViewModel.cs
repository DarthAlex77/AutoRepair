using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using AutoRepair.Model;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    internal class AutoEditWindowsViewModel : ReactiveObject
    {
        #region Constructors

        public AutoEditWindowsViewModel()
        {
        }

        public AutoEditWindowsViewModel(bool isAddMode)
        {
            _isAddMode = isAddMode;
        }

        #endregion

        #region IsAddModeProperty

        private bool _isAddMode;

        public bool IsAddMode
        {
            get => _isAddMode;
            set => this.RaiseAndSetIfChanged(ref _isAddMode, value);
        }

        #endregion

        #region CarManufacturerProperty

        private string _carManufacturer;
        public string CarManufacturer
        {
            get => _carManufacturer;
            set => this.RaiseAndSetIfChanged(ref _carManufacturer, value);
        }
        #endregion

        #region CarModelProperty

        private string _carModel;
        public string CarModel
        {
            get => _carModel;
            set => this.RaiseAndSetIfChanged(ref _carModel, value);
        }
        #endregion

        #region ColorProperty

        private Color _color;

        public Color Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        #endregion

        #region CarProduceYearProperty

        private string _carProduceYear;

        public string CarProduceYear
        {
            get => _carProduceYear;
            set => this.RaiseAndSetIfChanged(ref _carProduceYear, value);
        }

        #endregion

        #region CarNumberProperty

        private string _carNumber;

        public string CarNumber
        {
            get => _carNumber;
            set => this.RaiseAndSetIfChanged(ref _carNumber, value);
        }

        #endregion

        #region CarVINProperty

        private string _carVin;

        public string CarVin
        {
            get => _carVin;
            set => this.RaiseAndSetIfChanged(ref _carVin, value);
        }

        #endregion

        #region CarEngineNumberProperty

        private string _carEngineNumber;

        public string CarEngineNumber
        {
            get => _carEngineNumber;
            set => this.RaiseAndSetIfChanged(ref _carEngineNumber, value);
        }

        #endregion

        #region CarBodyNumberProperty

        private string _carBodyNumber;

        public string CarBodyNumber
        {
            get => _carBodyNumber;
            set => this.RaiseAndSetIfChanged(ref _carBodyNumber, value);
        }

        #endregion

        #region CarOwnerProperty

        private Client _owner;

        public Client CarOwner
        {
            get => _owner;
            set => this.RaiseAndSetIfChanged(ref _owner, value);
        }

        #endregion
    }
}