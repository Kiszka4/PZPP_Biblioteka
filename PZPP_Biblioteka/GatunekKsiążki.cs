using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    public class GatunekKsiążki
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public ICollection<Książka> Książki { get; set; }
    }
}
