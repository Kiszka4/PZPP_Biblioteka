using Microsoft.AspNetCore.Mvc;

namespace PZPP_BibliotekaAPI.Controllers
{
    [ApiController]
    [Route("api/ksiazki")]
    public class KsiazkiController : ControllerBase
    {
        private readonly BibliotekaContext _context;

        public KsiazkiController(BibliotekaContext context)
        {
            _context = context;
        }

        [HttpGet("dostepnosc")]
        public IActionResult Dostepnosc(string tytul)
        {
            var pasujace = _context.Ksiazki
                .Where(k => k.Tytul.Trim().ToLower() == tytul.Trim().ToLower())
                .ToList();

            if (!pasujace.Any())
            {
                //Console.WriteLine("BRAK DOPASOWANIA!");
                return Ok(-1); // do testów
            }

            var ilosc = pasujace.Sum(k => k.IloscNaStanie);
            return Ok(ilosc);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Ksiazki.ToList());
        }
    }
}
