using System;
using System.Windows;

namespace Repository
{
    public class ClassBox : ClassNotify
    {
        public ClassBox(ClassMaterial selectedMaterial)
        {
            SelectedMaterial = selectedMaterial;
            Height = "1000";
            Width = "1000";
            Depth = "1000";
        }

        private ClassMaterial _selectedMaterial;

        public ClassMaterial SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                if (_selectedMaterial != value)
                {
                    _selectedMaterial = value;
                    Notify(nameof(CalculateBuoyancyData));
                }
            }
        }


        private string _width;
        private string _height;
        private string _depth;

        public string Height
        {
            get { return _height; }
            set
            {
                if (_height != value && double.TryParse(value, out double _))
                {
                    _height = value;
                    Notify(nameof(CalculateOuterDimensions));
                    Notify(nameof(CalculateBuoyancyData));
                }
                Notify();
            }
        }

        public string Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    if (double.TryParse(value, out _))
                    {
                        _width = value;
                        Notify(nameof(CalculateOuterDimensions));
                        Notify(nameof(CalculateBuoyancyData));
                    }
                }
                Notify();
            }
        }

        public string Depth
        {
            get { return _depth; }
            set
            {
                if (_depth != value)
                {
                    if (double.TryParse(value, out _))
                    {
                        _depth = value;
                        Notify(nameof(CalculateOuterDimensions));
                        Notify(nameof(CalculateBuoyancyData));
                    }
                }
                Notify();
            }
        }

        private string _CalculateOuterDimensions;

        public string CalculateOuterDimensions
        {
            get
            {
                _CalculateOuterDimensions = CalculateDimensions();
                return _CalculateOuterDimensions;
            }
            set
            {
                Notify();
            }
        }

        private string CalculateDimensions()
        {
            if ((double.TryParse(Height, out double h) && h > 0) &&
                (double.TryParse(Width, out double w) && w > 0) &&
                (double.TryParse(Depth, out double d) && d > 0))
            {
                double volume = (h * w * d) / 1000000D;
                return $"{volume} m³";
            }
            else
            {
                return "0 m\u00b3";
            }
        }


        private string _CalculateBuoyancyData;

        public string CalculateBuoyancyData
        {
            get
            {
                _CalculateBuoyancyData = CalculateBuoyancy();
                return _CalculateBuoyancyData;
            }
            set
            {
                Notify();
            }
        }

        public string CalculateBuoyancy()
        {
            if ((double.TryParse(Height, out double h) && h > 0) &&
                (double.TryParse(Width, out double w) && w > 0) &&
                (double.TryParse(Depth, out double d) && d > 0) &&
                _selectedMaterial != null)
            {
                h /= 100; w /= 100; d /= 100;

                double outerVolume = h * w * d;

                double innerH = h - ((_selectedMaterial.Thickness / 1000) * 2);
                double innerW = w - ((_selectedMaterial.Thickness / 1000) * 2);
                double innerD = d - ((_selectedMaterial.Thickness / 1000) * 2);
                double innerVolume = innerH * innerW * innerD;

                double materialWeight = (outerVolume - innerVolume) * _selectedMaterial.Density * 1000;

                double displacementWeight = outerVolume * 1000;

                double carryingCapacity = displacementWeight - materialWeight;

                return carryingCapacity > 0 ? $"Bæreevne: {carryingCapacity:N2} kg" : "Kassen vil synke";
            }
            return "Ugyldig Data";
        }

    }
}