using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    public class Biblioteka : DbContext
    {
        public DbSet<Książka> Książki { get; set; }
        public DbSet<Gatunek> Gatunki { get; set; }
        //public DbSet<Zamowienie> Zamowienia { get; set; }
        //public DbSet<PozycjaZamowienia> PozycjeZamowienia { get; set; }
        public DbSet<Klient> Klienci { get; set; }

        public DbSet<Autor> Autorzy { get; set; }

        public Biblioteka(DbContextOptions<Biblioteka> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gatunek>()
                .HasMany(k => k.Książki)
                .WithOne(p => p.Gatuenk)
                .HasForeignKey(p => p.KategoriaProduktuID);

            modelBuilder.Entity<PozycjaZamowienia>()
                .HasOne(pz => pz.Zamowienie)
                .WithMany(z => z.PozycjeZamowienia)
                .HasForeignKey(pz => pz.ZamowienieID);
        }
    }
}
