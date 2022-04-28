using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLib
{
    public class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        private double a;
        private double b;
        private double c;
        public UnitOfMeasure Unit { get; set; }

        public static void CheckValue(double a, double b, double c) {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new ArgumentOutOfRangeException();
            else if (a > 10 || b > 10 || c > 10)
                throw new ArgumentOutOfRangeException();
        }

        public double A {
            get => a;
            set => a = Math.Round(value, 3);
        }

        public double B {
            get => b;
            set => b = Math.Round(value, 3);
        }

        public double C {
            get => c;
            set => c = Math.Round(value, 3);
        }

        public Pudelko(double a = 10, double b = 10, double c = 10, UnitOfMeasure unit = UnitOfMeasure.meter) {
            this.a = a;
            this.b = b;
            this.c = c;
            Unit = unit;

            CheckValue(a, b, c);
        }

        public double Objetosc => Math.Round((A * B * C), 9);

        public override bool Equals(object obj)
        {
            if (obj is Pudelko pudelko)
                return Equals(pudelko);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C);
        }

        public override string ToString()
        {
            return $"{A:0.000} m \u00D7 {B:0.000} m \u00D7 {C:0.000} m";
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (format == null)
                format = "m";

            return format switch
            {
                "m" => $"{A.ToString("#0.000", formatProvider)} m \u00D7 " +
                       $"{B.ToString("#0.000", formatProvider)} m \u00D7 " +
                       $"{C.ToString("#0.000", formatProvider)} m",

                "cm" => $"{A.ToString("#0.000", formatProvider)} cm \u00D7 " +
                        $"{B.ToString("#0.000", formatProvider)} cm \u00D7 " +
                        $"{C.ToString("#0.000", formatProvider)} cm",

                "mm" => $"{A.ToString("#0.000", formatProvider)} mm \u00D7 " +
                        $"{B.ToString("#0.000", formatProvider)} mm \u00D7 " +
                        $"{C.ToString("#0.000", formatProvider)} mm",

                _ => throw new NotImplementedException(),
            };
        }

        public bool Equals(Pudelko other)
        {
            if (other == null)
                return false;

            try
            {
                if (this.A == other.A || this.A == other.B || this.A == other.C)
                    if (this.B == other.A || this.B == other.B || this.B == other.C)
                        if(this.C == other.A || this.C == other.B || this.C == other.C)
                            return true;

                return false;
            }
            catch(NullReferenceException) {
                return false;
            }
        }

        //Do zrobienia
        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }


        public static bool operator ==(Pudelko p1, Pudelko p2) => Equals(p1, p2);
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);
        public static Pudelko operator +(Pudelko p1, Pudelko p2) {
            var tab = new double[]{ p1.A, p2.B, p1.C, p2.A, p2.B, p2.C };
            Array.Sort(tab);
            return new Pudelko(tab[0], tab[1], tab[2]);
        } 
        public static explicit operator double[](Pudelko p) => new double[] { p.a, p.b, p.c };
        public static implicit operator Pudelko(ValueTuple<int, int, int> p) => new(p.Item1, p.Item2, p.Item3, UnitOfMeasure.milimeter);

        public double this[int i]
        {
            get{
                if (i == 0)
                    return A;
                else{
                    if (i == 2)
                        return B;
                    else if(i == 3)
                        return C;
                    else
                        throw new IndexOutOfRangeException();
                }                  
            }
        }
        
    }
}
