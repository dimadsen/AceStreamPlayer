using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AceStreamPlayer;
using Xamarin.Forms;

namespace AceStreamPlayer
{
    public class LeagueViewModel:  BaseViewModel
    {
        
        public LeagueViewModel(List<Match> matches)
        {
            Matches = new ObservableCollection<Match>(matches);

        }


        private ObservableCollection<Match> matches;
        public ObservableCollection<Match> Matches
        {
            get
            {
                return matches;
            }
            set
            {
                matches = value;
            }
        }

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
