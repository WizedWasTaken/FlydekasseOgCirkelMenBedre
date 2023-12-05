using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Repository;

/// <summary>
/// Represents the business logic class for managing materials and boxes.
/// </summary>
namespace BIZ
{
    public class ClassBIZ : ClassNotify
    {
        public ObservableCollection<ClassMaterial> Materials { get; private set; }

        private ClassBox _box;
        private ClassCircle _circle;
        private ClassMaterial _selectedMaterial;

        public ClassBIZ()
        {
            Materials = new ObservableCollection<ClassMaterial>
            {
                new() { Name = "Træ", Density = 0.987, Thickness = 10 },
                new() { Name = "Plast", Density = 3.378, Thickness = 5 },
                new() { Name = "Jern", Density = 7.874, Thickness = 25 },
                new() { Name = "Bly", Density = 11.342, Thickness = 23 },
                new() { Name = "Guld", Density = 19.282, Thickness = 15 },
                new() { Name = "Sølv", Density = 10.49, Thickness = 12 },
                new() { Name = "Kobber", Density = 8.96, Thickness = 20 },
                new() { Name = "Aluminium", Density = 2.7, Thickness = 8 },
                new() { Name = "Stål", Density = 7.85, Thickness = 30 },
                new() { Name = "Glas", Density = 2.53, Thickness = 6 },
                new() { Name = "Beton", Density = 2.4, Thickness = 25 },
                new() { Name = "Hypotetisk Materiale", Density = 0.001, Thickness = 1 },
            };
            
            _selectedMaterial = Materials.FirstOrDefault();

            _circle = new ClassCircle(_selectedMaterial);
            _box = new ClassBox(_selectedMaterial);
        }



        public ClassBox Box
        {
            get { return _box; }
            set
            {
                _box = value;
            }
        }

        public ClassCircle Circle
        {
            get { return _circle; }
            set
            {
                _circle = value;
            }
        }

        public ClassMaterial SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                if (_selectedMaterial != value)
                {
                    _selectedMaterial = value;
                    Circle.SelectedMaterial = _selectedMaterial;
                    Box.SelectedMaterial = _selectedMaterial;
                    Notify();
                    Notify(nameof(Circle.CalculateBuoyancyData));
                    Notify(nameof(Box.CalculateBuoyancyData));
                }
            }
        }

        public void PrintData()
        {
            IEnumerable<ClassMaterial> materials = Materials;
            ClassPrint printer = new ClassPrint(materials);
            printer.StartPrint();

        }
    }
}



