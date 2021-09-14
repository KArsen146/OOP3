using System;
using System.Dynamic;
namespace c_sharp2
{
    public class Triangle
    {
        private double first;

        public double First
        {
            set
            {
                first = value;
            }
        }
        
        private double second;
        
        public double Second { get; set; }
        
        private double third;
        
        public double Third { get; set; }

        public Triangle() { }

        public Triangle(double _first, double _second, double _third)
        {
            first = _first;
            second = _second;
            third = _third;
        }

        public double Perimetr()
        {
            return first + second + third;
        }

        public double Square()
        {
            double p = (double) this.Perimetr()/ 2;
            return Math.Sqrt(p * (p - first) * (p - second) * (p - third));
        }
        
    }
}