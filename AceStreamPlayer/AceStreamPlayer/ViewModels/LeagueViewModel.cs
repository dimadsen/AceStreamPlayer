using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AceStreamPlayer
{
    public class LeagueViewModel
    {
        
        public LeagueViewModel(List<Match> matches)
        {
            Matches = new ObservableCollection<Match>(matches);
        }

        //ObservableCollection<Match> matches;
        public ObservableCollection<Match> Matches
        {
            get;
        }
    }
}
