using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _254008_L02
{
    public class ModelRate
    {
        [Key]
        public int? ID { set; get; }
        public float mid { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
    }
    public class ModelTable
    {
        [Key]
        public int? ID { set; get; }
        public string code { get; set; }
        public string currency { get; set; }
        public string table { get; set; }
        public List<ModelRate> rates { get; set; }
    }

    public class Serial
    {
        public Serial()
        {

        }

        public Serial( string _data )
        {
            data = _data;
        }

        [Key]
        public int? ID { set; get; }
        public string data { get; set; }
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

        public static Serial serialize(ModelTable table)
        {
            return new Serial(JsonConvert.SerializeObject(table));
        }

        public static ModelTable deserialize(string table)
        {
            return JsonConvert.DeserializeObject<ModelTable>(table);
        }
    }
}
