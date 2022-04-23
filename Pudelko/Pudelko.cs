using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pudelko
{
    public class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        private decimal a;
        private decimal b;
        private decimal c;
        public UnitOfMeasure Unit { get; set; }

        public void CheckValue(decimal a, decimal b, decimal c) {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException();
            else if (a > 10 || b > 10 || c > 10)
                throw new ArgumentOutOfRangeException();
        }

        public decimal A {
            get => a;
            set => a = Math.Round(value, 3);
        }

        public decimal B {
            get => b;
            set => b = Math.Round(value, 3);
        }

        public decimal C {
            get => c;
            set => c = Math.Round(value, 3);
        }

        public Pudelko(decimal a = 10, decimal b = 10, decimal c = 10, UnitOfMeasure unitOfMeasure = UnitOfMeasure.meter) {
            this.a = a;
            this.b = b;
            this.c = c;
            Unit = unitOfMeasure;

            CheckValue(a, b, c);
        }

        public double Objetosc => (double)Math.Round((A * B * C), 9);

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return format switch
            {
                "m" => $" {A.ToString("#0.000", formatProvider)} m x " +
                       $" {B.ToString("#0.000", formatProvider)} m x " +
                       $" {B.ToString("#0.000", formatProvider)} m x ",
                _ => throw new NotImplementedException()
            };
        }

        public bool Equals(Pudelko other)
        {
            throw new NotImplementedException();
        }
    }
}
