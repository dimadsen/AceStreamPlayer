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

        public INavigation Navigation { get; set; }

        Championat selectedChampionat;
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
        ObservableCollection<Championat> championats;
        public ObservableCollection<Championat> Championats
        {
            get
            {
                if (championats == null)
                {
                    List<Championat> champ = new List<Championat>()
                    {
                        new Championat()
                        {
                            Name = "Англия. Премьер-Лига",
                            Tour = "32 тур",
                        },
                        new Championat()
                        {
                            Name = "Россия. Премьер-Лига",
                            Tour = "26 тур"
                        }

                    };
                    championats = new ObservableCollection<Championat>(champ);
                }
                return championats;
            }

            set { championats = value; }
        }

        public void ShowMatches(Championat champ)
        {
            List<Match> matches = new List<Match>();

            switch (champ.Name)
            {
                case "Англия. Премьер-Лига":
                    matches.AddRange(new List<Match>{
                                new Match { Name = "Арсенал-Ливерпуль"}, new Match {Name = "Манчестер - Лестер"}
                            });
                    break;
                case "Россия. Премьер-Лига":
                    matches.AddRange(new List<Match>{
                                new Match { Name = "Ска - Динамо"}, new Match {Name = "Рубин - Локомотив"}
                            });
                    break;
            }

            Navigation.PushAsync(new LeaguePage(matches));
        }


        //Command showMatches;

        //public Command ShowCommand 
        //{
        //    get
        //    {
        //        return showMatches ?? (showMatches = new Command(obj =>
        //     {
        //         Championat champ = obj as Championat;

        //         List<Match> matches = new List<Match>();

        //         switch (champ.Name)
        //         {
        //             case "Англия. Премьер-Лига":
        //                 matches.AddRange(new List<Match>{
        //                        new Match { Name = "Арсенал-Ливерпуль"}, new Match {Name = "Манчестер - Лестер"}
        //                    });
        //                 break;
        //             case "Россия. Премьер-Лига":
        //                 matches.AddRange(new List<Match>{
        //                        new Match { Name = "Ска - Динамо"}, new Match {Name = "Рубин - Локомотив"}
        //                    });
        //                 break;
        //         }

        //         Navigation.PushAsync(new LeaguePage(matches));

        //     }));   
        //    }
        //}

    }

}

 
