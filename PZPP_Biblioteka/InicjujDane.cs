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

            var przymiotniki = new[] { "Cichy", "Mroczny", "Zapomniany" };
            var rzeczowniki = new[] { "Las", "Dom", "Sekret" };

            var random = new Random();
            var unikalneTytuly = new HashSet<string>();

            while (unikalneTytuly.Count < 9) // ile książek chcesz
            {

                var tytul = $"{przymiotniki[random.Next(przymiotniki.Length)]} {rzeczowniki[random.Next(rzeczowniki.Length)]}";

                unikalneTytuly.Add(tytul);
            }


            var ksiazki = unikalneTytuly.Select(t => new Książka
            {
                Tytuł = t,
                IloscNaStanie = random.Next(0, 10),
                ISBN = random.Next(100000000, 999999999),
                GatunekKsiążki = gatunki[random.Next(gatunki.Count)],
                Autor = autorzy[random.Next(autorzy.Count)]
            }).ToList();

            context.GatunkiKsiążek.AddRange(gatunki);
            context.Autorzy.AddRange(autorzy);
            context.Książki.AddRange(ksiazki);
            context.SaveChanges();
        }
    }
}
