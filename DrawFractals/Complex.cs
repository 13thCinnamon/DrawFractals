using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawFractals
{
    class Complex
    {
        public double real;
        public double imaginary;

        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public void Square()
        {
            double temp = (real * real) - (imaginary * imaginary);
            imaginary = 2.0 * real * imaginary;
            real = temp;
        }

        public double Magnitude()
        {
            return Math.Sqrt((real * real) + (imaginary * imaginary));
        }

        public void Add(Complex c)
        {
            real += c.real;
            imaginary += c.imaginary;
        }
    }
}
