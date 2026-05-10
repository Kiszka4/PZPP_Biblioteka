namespace PZPP_BibliotekaAPI
{
    public static class Seed
    {
        public static void Init(BibliotekaContext context)
        {
            if (context.Ksiazki.Any()) return;

            var przymiotniki = new [] { "Cichy", "Mroczny", "Zapomniany" };
            var rzeczowniki = new [] { "Las", "Dom", "Sekret" };

            var random = new Random();
            var unikalneTytuly = new HashSet<string>();

            while (unikalneTytuly.Count < 9) // ile książek chcesz
            {

                var tytul = $"{przymiotniki[random.Next(przymiotniki.Length)]} {rzeczowniki[random.Next(rzeczowniki.Length)]}";

                unikalneTytuly.Add(tytul);
            }

            var ksiazki = unikalneTytuly.Select(t => new Ksiazka
            {
                Tytul = t,
                IloscNaStanie = random.Next(0, 10)
            }).ToList();


            context.Ksiazki.AddRange(ksiazki);
            context.SaveChanges();
        }
    }
}
