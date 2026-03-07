using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    public class Książka
    {
        public int ISBN { get; set; }
        public string Tytuł { get; set; }
        public int IloscNaStanie { get; set; }

        public Gatunek Gatunek { get; set; }
        public Autor Autor { get; set; }
    }
}
