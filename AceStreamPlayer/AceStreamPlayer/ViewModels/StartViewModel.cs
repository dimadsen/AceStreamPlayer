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
        
        private Championat selectedChampionat;
        public Championat SelectedChampionat
        {
            get { return selectedChampionat; }
            set
            {
                if(value !=null)
                {
                    selectedChampionat = value;

                    OnPropertyChanged("selectedChampionat");
                    ShowMatches(selectedChampionat);
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
                   
                    List<Championat> champ  = App.DataBase.Table<Championat>().ToList(); 


                    championats = new ObservableCollection<Championat>(champ);
                }
                return championats;
            }

            set { championats = value; }
        }

        private void ShowMatches(Championat champ)
        {
            
            var matches = App.DataBase.Table<Match>().Where(m => m.ChampionatId == champ.Id).ToList();

            Navigation.PushAsync(new LeaguePage(matches)
            {
                Title = champ.Name
            });
        }

    }

}

 
