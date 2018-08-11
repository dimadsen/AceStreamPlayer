using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AceStreamPlayer
{
	public class StartViewModel : BaseViewModel
	{
		public StartViewModel()
		{
			refreshCommand = new Command(() =>
		   {
			   IsBusy = true;
			   Championats = RefreshList();
			   IsBusy = false;
			   
		   });

		}


		private Championat selectedChampionat;
		public Championat SelectedChampionat
		{
			get { return selectedChampionat; }
			set
			{
				if (value != null)
				{
					selectedChampionat = value;

					OnPropertyChanged("selectedChampionat");
					ShowMatches(selectedChampionat);
					//selectedChampionat = null;
				}

			}
		}
		private ObservableCollection<Championat> championats;
		public ObservableCollection<Championat> Championats
		{
			get
			{
				if (championats == null)
				{
					championats = App.Sql.GetCollection<Championat>();
				}
				return championats;
			}

			set
			{
				championats = value;
				OnPropertyChanged("Championats");
			}
		}

		private void ShowMatches(Championat champ)
		{
			var matches = App.Sql.GetCollection<Match>().Where(m => m.ChampionatId == champ.Id).ToList();

			Navigation.PushAsync(new LeaguePage(matches)
			{
				Title = champ.Name
			});

		}

		private ObservableCollection<Championat> RefreshList()
		{
			IsRefreshing = true;
			Worker.StartEventParse();
			IsRefreshing = false;
			return App.Sql.GetCollection<Championat>();
		}

	}

}


