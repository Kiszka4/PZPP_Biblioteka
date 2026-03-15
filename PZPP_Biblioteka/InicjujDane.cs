using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    public static class InicjujDane
    {
        public static void Inicjuj(Biblioteka context)
        {
            if (context.Książki.Any())
                return;

            var faker = new Faker("pl");

            var gatunki = new List<GatunekKsiążki>
            {
                new GatunekKsiążki { Nazwa = "Horror" },
                new GatunekKsiążki { Nazwa = "Obyczajowa" },
                new GatunekKsiążki { Nazwa = "Romans" },
                new GatunekKsiążki { Nazwa = "Sci-Fi" },
                new GatunekKsiążki { Nazwa = "Fantasy" },
                new GatunekKsiążki { Nazwa = "Thriller" },
                new GatunekKsiążki { Nazwa = "Kryminał" },
                new GatunekKsiążki { Nazwa = "Biografia" },
                new GatunekKsiążki { Nazwa = "Historia" }
            };

            var autorzy = new Faker<Autor>()
                .RuleFor(k => k.Imię, f => f.Name.FirstName())
                .RuleFor(k => k.Nazwisko, f => f.Name.LastName())
                .Generate(20);

            var książki = new Faker<Książka>("pl")
                .RuleFor(k => k.Tytuł, f => f.Lorem.Sentence(3))
                .RuleFor(p => p.IloscNaStanie, f => f.Random.Int(0, 40))
                .RuleFor(p => p.ISBN, f => f.Random.Int(100000000, 999999999))
                .RuleFor(p => p.GatunekKsiążki, f => f.PickRandom(gatunki))
                .RuleFor(p => p.Autor, f => f.PickRandom(autorzy))
                .Generate(20);

            context.GatunkiKsiążek.AddRange(gatunki);
            context.Autorzy.AddRange(autorzy);
            context.Książki.AddRange(książki);
            context.SaveChanges();
        }
    }
}
