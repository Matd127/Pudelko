using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLib
{
    public static class PudelkoCompressed
    {
        public static Pudelko Kompresuj(Pudelko p)
        {
            var b = Math.Cbrt(p.Objetosc);
            return new Pudelko(b, b, b);
        }
    }
}
