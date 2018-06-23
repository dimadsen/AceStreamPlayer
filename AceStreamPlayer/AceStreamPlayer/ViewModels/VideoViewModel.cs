using System;
using System.Net.Http;
using System.Threading.Tasks;
using FormsVideoLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AceStreamPlayer
{
    public class VideoViewModel : BaseViewModel
    {

        public VideoViewModel(Reference reference, VideoPlayer videoPlayer)
        {
            WatchMatch(videoPlayer, reference);
        }


        private async void WatchMatch(VideoPlayer player, Reference reference)
        {
            var url = await GetUrl(reference.ContentId);
            player.Source = new UriVideoSource()
            {
                Uri = url?.playback_url
            };
        }

        private async Task<Response> GetUrl(string contentId)
        {
            try
            {
                var uri = $"http://192.168.1.37:6878/ace/manifest.m3u8?format=json&id={contentId}";

                var client = new HttpClient();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

                client.BaseAddress = new Uri(uri);

                var response = await client.GetAsync(new Uri(uri));
                response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

                var content = await response.Content.ReadAsStringAsync();
				var json = JObject.Parse(content).SelectToken(@"$.response");

                var url = JsonConvert.DeserializeObject<Response>(json.ToString());

                return url;
            }
			catch(Exception ex)
            {
                return null;
            }
        }


    }
}
