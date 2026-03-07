using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    public class Gatunek
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public ICollection<Gatunek> Gatunki { get; set; }
    }
}
