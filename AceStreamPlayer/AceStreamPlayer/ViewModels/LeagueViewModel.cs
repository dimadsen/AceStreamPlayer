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
			DateTime date;
			string minute;
            matches.ForEach(m =>
            {
                m.Name = $"{m.Hosts} - {m.Visitors}";
                
                if (m.Status == "Live")
                    m.Time = "LIVE";
                else
				{
					date = DateTime.Parse(m.Date);
					minute = date.Minute == 0 ? minute = "00" : date.Minute.ToString();
					m.Time = $"Начало в {date.Hour}:{minute}";
				}
					
            });

            return matches.Where(m => m.Status != "Завершён").ToList();
        }

        private void ShowReferences(Match match)
        {
            var references = App.DataBase.Table<Reference>().Where(m => m.MatchId == match.Id).ToList();

            Navigation.PushAsync(new BroadcastPage(references, match)
            {
                Title = "Трансляция матча"
            });

        }


    }
}
