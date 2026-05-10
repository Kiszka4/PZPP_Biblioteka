using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZPP_Biblioteka
{
    using System.Net.Http;

    public class ApiService
    {
        private readonly HttpClient _http = new HttpClient();

        public async Task<int> PobierzDostepnosc(string tytul)
        {
            //var encoded = Uri.EscapeDataString(tytul);
            //var url = $"http://localhost:5053/api/ksiazki/dostepnosc?tytul={encoded}";
            var url = $"http://localhost:5053/api/ksiazki/dostepnosc?tytul={tytul}";
            var response = await _http.GetStringAsync(url);

            return int.Parse(response);
        }
    }
}
