using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace PZPP_Biblioteka
{
    public static class InicjujDane
    {
        public static void Inicjuj(Biblioteka context)
        {
            var faker = new Faker("pl");

            var gatunki = new List<Gatunek>
        {
            new Gatunek { Nazwa = "Horror" },
            new Gatunek { Nazwa = "Obyczajowa" },
            new Gatunek { Nazwa = "Romans" },
            new Gatunek { Nazwa = "Sci-Fi" },
            new Gatunek { Nazwa = "Fantasy" },
            new Gatunek { Nazwa = "Thriller" },
            new Gatunek { Nazwa = "Kryminał" },
            new Gatunek { Nazwa = "Biografia" },
            new Gatunek { Nazwa = "Historia" }

        };

            var książki = new Faker<Książka>()
                .RuleFor(p => p.Tytuł, faker => faker.Lorem.Sentence(3, 5))
                .RuleFor(p => p.IloscNaStanie, f => f.Random.Int(0, 40))
                //.RuleFor(p => p.CenaJednostkowa, f => Math.Round(f.Random.Double(1, 3000), 2))
                //.RuleFor(p => p.VAT, f => Math.Round(f.Random.Double(0.05, 0.23), 2))
                .RuleFor(p => p.Gatunek, f => f.PickRandom(gatunki))
                .Generate(20);

            var autorzy = new Faker<Autor>()
                .RuleFor(k => k.Imię, f => f.Name.FirstName())
                .RuleFor(k => k.Nazwisko, f => f.Name.LastName())
                .RuleFor(k => k.ID, f => f.Random.Int(0000, 9999))
                .Generate(20);


            context.Gatunki.AddRange(gatunki);
            context.Książki.AddRange(książki);
            context.Autorzy.AddRange(autorzy);
            context.SaveChanges();

            /*
            var pozycjeZamowienia = new Faker<PozycjaZamowienia>()
                .CustomInstantiator(f => new PozycjaZamowienia())
                .RuleFor(p => p.Ilosc, f => f.Random.Int(1, 10))
                .RuleFor(p => p.Znizka, f => Math.Round(f.Random.Double(0, 0.5), 2))
                .RuleFor(p => p.CenaJednostkowa, f => Math.Round(f.Random.Double(1, 3000), 2))
                .RuleFor(p => p.Produkt, f => f.PickRandom(produkty))
                .Generate(40);

            var zamowienia = new Faker<Zamowienie>()
                .CustomInstantiator(f => new Zamowienie())
                .RuleFor(z => z.DataRozpoczecia, f => f.Date.Recent())
                .RuleFor(z => z.DataZakonczenia, (f, z) => z.DataRozpoczecia.AddDays(f.Random.Int(1, 10)))
                .RuleFor(z => z.Status, f => f.PickRandom<Status>())
                .RuleFor(z => z.Klient, f => f.PickRandom(klienci))
                .Generate(10);



            foreach (var pozycja in pozycjeZamowienia)
            {
                pozycja.Zamowienie.PozycjeZamowienia.Add(pozycja);
            }


            context.Zamowienia.AddRange(zamowienia);
            context.PozycjeZamowienia.AddRange(pozycjeZamowienia);

            context.SaveChanges();
            */

        }
    }
}
