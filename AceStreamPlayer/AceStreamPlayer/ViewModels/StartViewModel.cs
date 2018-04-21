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
                            Name = "Англия.Премьер-Лига",
                            Tour = "32 тур",
                        },
                        new Championat()
                        {
                            Name = "Россия.Премьер-Лига",
                            Tour = "26"
                        }

                    };
                    championats = new ObservableCollection<Championat>(champ);
                }
                return championats;
            }

            set { championats = value; }
        }

        
    }
}
