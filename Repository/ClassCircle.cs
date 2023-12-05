using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Repository
{
    public class ClassCircle : ClassNotify
    {
        public ClassCircle(ClassMaterial selectedMaterial)
        {
            SelectedMaterial = selectedMaterial;
            Height = "1000";
            Radius = "1000";
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
                    Notify();
                    Notify(nameof(CalculateBuoyancyData));
                    // Debugging
                }
            }
        }

        private string _height;
        private string _radius;

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

        public string Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                {
                    if (double.TryParse(value, out _))
                    {
                        _radius = value;
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
                (double.TryParse(Radius, out double r) && r > 0))
            {
                double volume = Math.PI * r * r * h / 1000000D;
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
                _CalculateBuoyancyData = CalculateBuoyancy(SelectedMaterial);
                return _CalculateBuoyancyData;
            }
            set
            {
                Notify();
            }
        }

        public string CalculateBuoyancy(ClassMaterial selectedMaterial)
        {
            if (selectedMaterial != null &&
                double.TryParse(Height, out double h) && h > 0 &&
                double.TryParse(Radius, out double r) && r > 0)
            {
                h /= 100;
                r /= 100;

                double outerVolume = Math.PI * r * r * h;
                double innerR = r - (selectedMaterial.Thickness / 1000);
                double innerVolume = Math.PI * innerR * innerR * h;

                double materialWeight = (outerVolume - innerVolume) * selectedMaterial.Density * 1000;
                double displacementWeight = outerVolume * 1000;
                double carryingCapacity = displacementWeight - materialWeight;

                return carryingCapacity > 0 ? $"Bæreevne: {carryingCapacity:N2} kg" : "Kassen vil synke";
            }
            return "Ugyldig Data";
        }

    }
}
