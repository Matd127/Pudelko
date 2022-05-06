using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLib
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        private double a;
        private double b;
        private double c;
        public UnitOfMeasure Unit { get; set; }

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

        public static void CheckValue(double a, double b, double c){
            if (a < 0.001 || b < 0.001 || c < 0.001)
                throw new ArgumentOutOfRangeException();

            if (a > 10 || b > 10 || c > 10)
                throw new ArgumentOutOfRangeException();
        }

        public static double SetValue(UnitOfMeasure unit, double val) {
            if (unit == UnitOfMeasure.centimeter)
                return val /= 100;
            else if (unit == UnitOfMeasure.milimeter)
                return val /= 1000;
            else
                return val *= 1;
        }

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter) {
            Unit = unit;

            this.a = a == null ? 0.1 : SetValue(unit, (double)a);
            this.b = b == null ? 0.1 : SetValue(unit, (double)b);
            this.c = c == null ? 0.1 : SetValue(unit, (double)c);
                
            CheckValue(A, B, C);
           
        }

        public double Objetosc => Math.Round((A * B * C), 9);
        public double Pole => Math.Round((A * B * 2) + (A * C * 2) + (B * C * 2), 6);

        public override bool Equals(object obj){
            if (obj is Pudelko pudelko)
                return Equals(pudelko);
            else
                return false;
        }

        public override int GetHashCode(){
            return HashCode.Combine(A, B, C);
        }

        public override string ToString() => $"{A:0.000} m \u00D7 {B:0.000} m \u00D7 {C:0.000} m";

        public string ToString(string format){
            return ToString(format, CultureInfo.GetCultureInfo("en-US"));
        }

        public string ToString(string format, IFormatProvider formatProvider = null){
            if (formatProvider is null)
                formatProvider = CultureInfo.CurrentCulture;

            if (format == null)
                format = "m";

            return format switch
            {
                "m" => $"{A.ToString("#0.000", formatProvider)} m × " +
                       $"{B.ToString("#0.000", formatProvider)} m × " +
                       $"{C.ToString("#0.000", formatProvider)} m",

                "cm" => $"{(A*100).ToString("#0.0", formatProvider)} cm × " +
                        $"{(B*100).ToString("#0.0", formatProvider)} cm × " +
                        $"{(C*100).ToString("#0.0", formatProvider)} cm",

                "mm" => $"{(A * 1000).ToString("#0", formatProvider)} mm × " +
                        $"{(B * 1000).ToString("#0", formatProvider)} mm × " +
                        $"{(C * 1000).ToString("#0", formatProvider)} mm",

                _ => throw new FormatException(),
            };
        }

        public bool Equals(Pudelko other){
            if (other == null)
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            return (Pole == other.Pole && Objetosc == other.Objetosc);
        }

        public IEnumerator GetEnumerator(){
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

        public double this[int index]{
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

        public static Pudelko Parse(string p){
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
