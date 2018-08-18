using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AceStreamPlayer.AdditionalClasses
{
    public static class HttpWorker
    {
        public async static Task<T> GetAceStreamUrl<T>(string uri)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

                client.BaseAddress = new Uri(uri);

                var response = await client.GetAsync(new Uri(uri));
                response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content).SelectToken($"$.response");

                var url = JsonConvert.DeserializeObject<T>(json.ToString());

                return url;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async static Task<IHtmlDocument> GetReferencesTable(string url)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

            client.BaseAddress = new Uri(url);

            var response = client.GetAsync(new Uri(url));
            //response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

            var content = response.Result.Content.ReadAsStringAsync();


            var json = JObject.Parse(content.Result).SelectToken($"$.broadcast").ToString();

            var parser = new HtmlParser();
            var doc = parser.Parse(json);

            return doc;

        }
    }
}
