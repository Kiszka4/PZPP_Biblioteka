using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    public class Autor
    {
        public int ID { get; set; }
        public string Imię { get; set; }
        public string Nazwisko { get; set; }
        public string PełneNazwisko => $"{Imię} {Nazwisko}";
        public List<Książka> Książki { get; set; }
    }
}
