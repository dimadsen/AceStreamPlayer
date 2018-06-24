using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;


		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region Methods
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

		protected ObservableCollection<T> GetCollection<T>() where T : new()
		{
			var list = App.DataBase.Table<T>().ToList();
			var collection = new ObservableCollection<T>(list);

			return collection;
		}

		protected async Task GetIdUrl()
		{
			var url = "https://livesport.ws/league/2018-fifa-world-cup";
			var config = Configuration.Default.WithDefaultLoader();
			var document = await BrowsingContext.New(config).OpenAsync(url);

			var hrefs = document.QuerySelectorAll("a").Where(x => x.ClassName == "dark-btn").ToList();

			var urls = new List<string>();

			hrefs.ForEach(href => urls.Add((string)href.GetType().GetProperty("Href").GetValue(href)));

		}

		#endregion

		#region Properties

		protected bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged(nameof(IsBusy));
			}
		}  		protected bool _isRefreshing;
		public bool IsRefreshing
		{
			get { return _isRefreshing; }
			set
			{
				_isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}

		public INavigation Navigation { get; set; }

		#endregion 
		#region Commands
		protected Command _refreshCommand;
		public Command RefreshCommand
		{
			get
			{
				return _refreshCommand;
			}
		}
		#endregion
	}
}
