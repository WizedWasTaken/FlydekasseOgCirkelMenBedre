using System;
using System.Windows;

namespace Repository
{
    public class ClassMaterial : ClassNotify
    {
        public ClassMaterial()
        {
        }

        public string Name { get; set; }
        public double Density { get; set; }

        private double _thickness;
        public double Thickness
        {
            get { return _thickness; }
            set
            {
                if (_thickness != value)
                {
                    _thickness = value;
                    Notify();
                }
            }
        }
    }
}