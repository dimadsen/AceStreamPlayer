using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using FormsVideoLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Internals;

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
			 var url =  await GetUrl(reference.ContentId);
		    player.Source = new UriVideoSource()
			{
				Uri = url?.playback_url
			};
		}
        
		private  async Task<Response>  GetUrl(string contentId)
		{
			string url = $"http://192.168.1.37:6878/ace/manifest.m3u8?format=json&id={contentId}";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

            client.BaseAddress = new Uri(url);

			var response = await client.GetAsync(new Uri(url));
            response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

			var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var j = json.SelectToken(@"$.response");
			var ace = JsonConvert.DeserializeObject<Response>(j.ToString());

			return ace;

		}


    }
}
