using System;
using System.Collections.Generic;
namespace PudelkoLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Pudelko p = new Pudelko(0.1, unit: UnitOfMeasure.milimeter);
            Console.WriteLine(p.ToString("mm"));


        }
    }
}
