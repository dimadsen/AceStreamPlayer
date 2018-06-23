using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace AceStreamPlayer
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public INavigation Navigation { get; set; }

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		protected async Task<T> GetUrl<T>(string uri)
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
				var json = JObject.Parse(content).SelectToken($"$.{typeof(T).Name}");

				var url = JsonConvert.DeserializeObject<T>(json.ToString());

				return url;
			}
			catch
			{
				return default(T);
			}
		}

	}
}
