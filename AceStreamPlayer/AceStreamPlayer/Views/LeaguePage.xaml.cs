using System.Collections.Generic;
using Xamarin.Forms;

namespace AceStreamPlayer
{
    public partial class LeaguePage : ContentPage
    {
        public LeaguePage(List<Match> matches)
        {
            InitializeComponent();
            BindingContext = new LeagueViewModel(matches)
            {
                Navigation = Navigation
            };
        }
    }
}
