using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using ReactiveUI;

namespace AutoRepair.Model
{
    public class Car : ReactiveObject
    {
        #region Construcotrs

        public Car()
        {
        }

        public Car(CarModel carModel, Color color, DateTime carProduceYear, string carNumber, string carVin,
            string carEngineNumber, string carBodyNumber, Client owner)
        {
            _carModel = carModel;
            _color = color;
            _carProduceYear = carProduceYear;
            _carNumber = carNumber;
            _carVin = carVin;
            _carEngineNumber = carEngineNumber;
            _carBodyNumber = carBodyNumber;
            _owner = owner;
        }

        #endregion

        #region CarIdProperty

        private int _carId;
        public int CarId
        {
            get => _carId;
            set => this.RaiseAndSetIfChanged(ref _carId, value);
        }

        #endregion


        #region CarModelProperty

        private CarModel _carModel;

        public CarModel CarModel
        {
            get => _carModel;
            set => this.RaiseAndSetIfChanged(ref _carModel, value);
        }

        #endregion

        #region ColorProperty
        [Browsable(false), Column("Color")]
        public int Argb {
            get => _color.ToArgb();
            set => _color = Color.FromArgb(value);
        }

        private Color _color;
        [NotMapped]
        public Color Color
        {
            get => _color;
            set => this.RaiseAndSetIfChanged(ref _color, value);
        }

        #endregion

        #region CarProduceYearProperty

        private DateTime _carProduceYear;

        public DateTime CarProduceYear
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