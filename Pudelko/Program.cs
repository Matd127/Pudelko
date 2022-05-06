using System;
using System.Collections.Generic;
namespace PudelkoLib
{
    class Program
    {
        private static int Comparison(Pudelko p1, Pudelko p2)
        {
            int objetosc = p1.Objetosc.CompareTo(p2.Objetosc);
            if (objetosc != 0)
                return objetosc;
            else{
                int pole = p1.Pole.CompareTo(p2.Pole);

                if (pole != 0)
                    return pole;
                else {
                    double bokip1 = p1.A + p1.B + p1.C;
                    double bokip2 = p2.A + p2.B + p2.C;

                    int sum = bokip1.CompareTo(bokip2);
                    if (sum != 0)
                        return sum;
                    else
                        return 1;
                }
            }
        }

        static void Main(string[] args)
        {
            var pudelka = new List<Pudelko>
            {
                new(8, 9, 9.97, UnitOfMeasure.meter),
                new(1, 2.46, 6, UnitOfMeasure.meter),
                new(7, 8, 12, UnitOfMeasure.centimeter),
                new(18, 2, 19, UnitOfMeasure.centimeter),
                new(18, 2, 19, UnitOfMeasure.centimeter),
                new(8, 18, 20, UnitOfMeasure.milimeter)              
            };

            foreach (var item in pudelka) {
                Console.WriteLine(item);
            }

            pudelka.Sort(Comparison);

            Console.WriteLine();
            foreach (var p in pudelka){
                Console.WriteLine(p.ToString());
            }


        }
    }
}
