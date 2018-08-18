using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
					var match = value;
					selectedMatch = null;

					OnPropertyChanged(nameof(SelectedMatch));
					ShowReferences(match);
				}
			}
		}


		private List<Match> SetNameAndTime(List<Match> matches)
		{
			matches.ForEach(match =>
			{
				match.Name = $"{match.Hosts} - {match.Visitors}";

				if (match.Status == "LIVE")
					match.Time = "LIVE";
				else
					match.Time = GetTime(match.Date);
			});

			return matches.Where(m => m.Status != "Завершен" && DateParse(m.Date).Day == DateTime.Now.Day).ToList();
		}

		private void ShowReferences(Match match)
		{
			var references = App.Sql.GetCollection<Reference>().Where(m => m.MatchId == match.Id).ToList();

			Navigation.PushAsync(new BroadcastPage(references, match)
			{
				Title = "Трансляция матча"
			});

		}

		private string GetTime(string matchDate)
		{
			var date = DateTime.ParseExact(matchDate, "dd MMMM yyyy, HH:mm", new CultureInfo("ru-RU"));
			var minute = date.Minute == 0 ? "00" : date.Minute.ToString();
			return $"Начало в {date.Hour}:{minute}";
		}

		private static DateTime DateParse(string matchDate)
        {
            var date = DateTime.ParseExact(matchDate, "dd MMMM yyyy, HH:mm", new CultureInfo("ru-RU"));
            return date;
        }

	}
}
