using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PZPP_Biblioteka
{
    public class Filia
    {
        [Key]
        public int ID { get; set; }
        public string? Nazwa { get; set; }
        public string? Lokalizacja { get; set; }

        public ICollection<StanMagazynowy> StanyMagazynowe { get; set; } = new List<StanMagazynowy>();
    }
}