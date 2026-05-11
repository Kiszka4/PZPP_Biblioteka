using System.ComponentModel.DataAnnotations;

namespace PZPP_Biblioteka
{
    public class StanMagazynowy
    {
        [Key]
        public int ID { get; set; }
        public int IloscNaStanie { get; set; }

        public int KsiążkaISBN { get; set; }
        public Książka Książka { get; set; } = null!;

        public int FiliaID { get; set; }
        public Filia Filia { get; set; } = null!;
    }
}