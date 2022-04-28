using System;
using System.Collections.Generic;
namespace PudelkoLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Pudelko pudelko = new Pudelko(15, 6, 3, UnitOfMeasure.meter);
            Console.WriteLine(pudelko);

        }
    }
}
