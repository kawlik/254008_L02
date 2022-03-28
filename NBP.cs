using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _254008_L02
{
    public class ModelRate
    {
        public float mid { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
    }

    public class ModelTable
    {
        public string code { get; set; }
        public string currency { get; set; }
        public string table { get; set; }
        public List<ModelRate> rates { get; set; }
    }

    public static class NBP
    {
        private static string GetURI( string currency, string date )
        {
            return $"http://api.nbp.pl/api/exchangerates/rates/a/{ currency }/{ date }/?format=json";
        }

        public static async Task<ModelTable> GetTable( string currency, string date )
        {
            ModelTable table = null;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NBP.GetURI(currency, date)),
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();

                table = JsonConvert.DeserializeObject<ModelTable>(body);
            }

            return table;
        }
    }
}
