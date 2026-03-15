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
        public DbSet<GatunekKsiążki> GatunkiKsiążek { get; set; }
        public DbSet<Autor> Autorzy { get; set; }

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
        }
    }
}
