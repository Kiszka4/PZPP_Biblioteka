namespace PZPP_BibliotekaAPI
{
    using Microsoft.EntityFrameworkCore;

    public class BibliotekaContext : DbContext
    {
        public DbSet<Ksiazka> Ksiazki { get; set; }

        public BibliotekaContext(DbContextOptions<BibliotekaContext> options)
            : base(options)
        {
        }
    }
}
