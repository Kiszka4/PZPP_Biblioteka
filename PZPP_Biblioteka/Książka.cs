using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PZPP_Biblioteka
{
    public class Książka
    {
        [Key]
        public int ISBN { get; set; }
        public string? Tytuł { get; set; }
        public int IloscNaStanie { get; set; }

        public Autor? Autor { get; set; }
        public int? AutorID { get; set; }
        public GatunekKsiążki? GatunekKsiążki { get; set; }
        public int GatunekID { get; set; }
        public int DostepnoscWBibliotece2 { get; set; }

        public ICollection<StanMagazynowy> StanyMagazynowe { get; set; } = new List<StanMagazynowy>();
    }
}