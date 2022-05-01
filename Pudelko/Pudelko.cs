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
            if (a < 0.001 || b < 0.001 || c < 0.001)
                throw new ArgumentOutOfRangeException();

            if (a > 10 || b > 10 || c > 10)
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

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter) {

            if (unit == UnitOfMeasure.meter) {
                this.a = a == null ? 0.1 : (double)a;
                this.b = b == null ? 0.1 : (double)b;
                this.c = c == null ? 0.1 : (double)c;
                Unit = unit;
            }

            if (unit == UnitOfMeasure.centimeter)
            {
                this.a = a == null ? 0.1 : (double)a/100;
                this.b = b == null ? 0.1 : (double)b/100;
                this.c = c == null ? 0.1 : (double)c/100;
                Unit = unit;
            }

            if (unit == UnitOfMeasure.milimeter)
            {
                this.a = a == null ? 0.1 : (double)a / 1000;
                this.b = b == null ? 0.1 : (double)b / 1000;
                this.c = c == null ? 0.1 : (double)c / 1000;
                Unit = unit;       
            }
            CheckValue(A, B, C);
           
        }

        public double Objetosc => Math.Round((A * B * C), 9);
        public double Pole => Math.Round((A * B * 2) + (A * C * 2) + (B * C * 2), 6);

        public override bool Equals(object obj)
        {
            if (obj is Pudelko pudelko)
                return Equals(pudelko);
            else
                return false;
        }

        public override int GetHashCode(){
            return HashCode.Combine(A, B, C);
        }

        public override string ToString() => $"{A:0.000} m \u00D7 {B:0.000} m \u00D7 {C:0.000} m";

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.GetCultureInfo("en-US"));
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider is null){
                formatProvider = CultureInfo.CurrentCulture;
            }

            if (format == null)
                format = "m";

            return format switch
            {
                "m" => $"{A.ToString("#0.000", formatProvider)} m \u00D7 " +
                       $"{B.ToString("#0.000", formatProvider)} m \u00D7 " +
                       $"{C.ToString("#0.000", formatProvider)} m",

                "cm" => $"{(A*100).ToString("#0.0", formatProvider)} cm \u00D7 " +
                        $"{(B*100).ToString("#0.0", formatProvider)} cm \u00D7 " +
                        $"{(C*100).ToString("#0.0", formatProvider)} cm",

                "mm" => $"{(A * 1000).ToString("#0", formatProvider)} mm \u00D7 " +
                        $"{(B * 1000).ToString("#0", formatProvider)} mm \u00D7 " +
                        $"{(C * 1000).ToString("#0", formatProvider)} mm",

                _ => throw new FormatException(),
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

        public IEnumerator GetEnumerator()
        {
            double[] p = new double[] { this.A, this.B, this.C };
            foreach(var wymiar in p)
                yield return wymiar;
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

        public double this[int index]
        {
            get{
                if (index == 0)
                    return A;
                else{
                    if (index == 1)
                        return B;
                    else if(index == 2)
                        return C;
                    else
                        throw new IndexOutOfRangeException();
                }                  
            }
        }

        public static Pudelko Parse(string p) {
            string[] kr = p.Split(" ");
            double a = double.Parse(kr[0].Replace('.', ','));
            double b = double.Parse(kr[3].Replace('.', ','));
            double c = double.Parse(kr[6].Replace('.', ','));

            if (kr[1] == "m")
                return new Pudelko(a, b, c, UnitOfMeasure.meter);
            else if (kr[1] == "cm")
                return new Pudelko(a, b, c, UnitOfMeasure.centimeter);
            else if (kr[1] == "mm")
                return new Pudelko(a, b, c, UnitOfMeasure.milimeter);
            else
                throw new ArgumentException();

        }
    }
}
