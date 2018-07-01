using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
	public class LeagueViewModel : BaseViewModel
	{
		public LeagueViewModel(List<Match> matches)
		{
			Matches = new ObservableCollection<Match>(SetNameAndTime(matches));
		}

		public ObservableCollection<Match> Matches { get; set; }

		private Match selectedMatch;
		public Match SelectedMatch
		{
			get { return selectedMatch; }
			set
			{
				if (value != null)
				{
					selectedMatch = value;

					OnPropertyChanged("selectedMatch");
					ShowReferences(selectedMatch);
				}
			}
		}


		private List<Match> SetNameAndTime(List<Match> matches)
		{
			foreach (var match in matches)
			{
				match.Name = $"{match.Hosts} - {match.Visitors}";
				if (match.Status == "LIVE")
					match.Time = "LIVE";
				else
				{
					var date = DateTime.Parse(match.Date);
					var minute = date.Minute == 0 ? "00" : date.Minute.ToString();
					match.Time = $"Начало в {date.Hour}:{minute}";
				}
			}
			return matches.Where(m => m.Status != "Завершён").ToList();
		}

		private void ShowReferences(Match match)
		{
			var references = DataBase.Sql.Table<Reference>().Where(m => m.MatchId == match.Id).ToList();

			Navigation.PushAsync(new BroadcastPage(references, match)
			{
				Title = "Трансляция матча"
			});

		}


	}
}
