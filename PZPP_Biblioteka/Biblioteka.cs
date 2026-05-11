using Microsoft.EntityFrameworkCore;

namespace PZPP_Biblioteka
{
    public class Biblioteka : DbContext
    {
        public DbSet<Książka> Książki { get; set; }
        public DbSet<GatunekKsiążki> GatunkiKsiążek { get; set; }
        public DbSet<Autor> Autorzy { get; set; }
        public DbSet<Filia> Filie { get; set; }
        public DbSet<StanMagazynowy> StanyMagazynowe { get; set; }

        public Biblioteka(DbContextOptions<Biblioteka> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GatunekKsiążki>()
                .HasMany(k => k.Książki)
                .WithOne(p => p.GatunekKsiążki)
                .HasForeignKey(p => p.GatunekID);

            modelBuilder.Entity<Autor>()
                .HasMany(a => a.Książki)
                .WithOne(k => k.Autor)
                .HasForeignKey(k => k.AutorID);

            modelBuilder.Entity<StanMagazynowy>()
                .HasOne(s => s.Książka)
                .WithMany(k => k.StanyMagazynowe)
                .HasForeignKey(s => s.KsiążkaISBN);

            modelBuilder.Entity<StanMagazynowy>()
                .HasOne(s => s.Filia)
                .WithMany(f => f.StanyMagazynowe)
                .HasForeignKey(s => s.FiliaID);
        }
    }
}