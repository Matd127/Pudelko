using System;

namespace Pudelko
{
    class Program
    {
        static void Main(string[] args)
        {
            Pudelko pudelko = new Pudelko(5, 5, 5, UnitOfMeasure.meter);
            Console.WriteLine(pudelko.A);
            Console.WriteLine(pudelko.B);
            Console.WriteLine(pudelko.C);
            Console.WriteLine(pudelko.ToString());
        }
    }
}
