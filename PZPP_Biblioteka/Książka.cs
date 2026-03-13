using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    public class Książka
    {
        [Key]
        public int ISBN { get; set; }
        public string Tytuł { get; set; }
        public int IloscNaStanie { get; set; }

        public GatunekKsiążki GatunekKsiążki { get; set; }
        public Autor Autor { get; set; }

        public int GatunekID { get; set; }
    }
}
