using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AceStreamPlayer
{
    public class BroadcastViewModel : BaseViewModel
    {
        private Match _match;

		public ICommand WatchCommand { get; set; } 

        public BroadcastViewModel(List<Reference> references, Match match)
        {
            References = new ObservableCollection<Reference>(references);
            _match = match;

        }

        private ObservableCollection<Reference> references;

        public ObservableCollection<Reference> References
        {
            get
            {
                return references;
            }
            set
            {
                references = value;
            }
        }
        private Reference selectedReference;
        public Reference SelectedReference
        {
            get { return selectedReference; }
            set
            {
                if (value != null)
                {
                    selectedReference = value;

                    OnPropertyChanged("selectedReference");
					WatchMatch(selectedReference);
                }
            }
        }
		private async void WatchMatch(Reference reference)
        {
			await Navigation.PushAsync(new VideoPage(reference));
        }


    #region Properties

        public string Hosts
        {
            get { return _match.Hosts; }
            set
            {

                if (_match.Hosts != value)
                {
                    _match.Hosts = value;
                    OnPropertyChanged("Hosts");
                }

            }
        }

        public string Visitors
        {
            get { return _match.Visitors; }
            set
            {
                if (_match.Visitors != null)
                {
                    _match.Visitors = value;
                    OnPropertyChanged("Visitors");
                }

            }
        }

        public string PictureHosts
        {
            get { return _match.PictureHosts; }
            set
            {
                if (_match.PictureHosts != value)
                {
                    _match.PictureHosts = value;
                    OnPropertyChanged("PictureHosts");
                }
            }
        }

        public string PictureVisitors
        {
            get { return _match.PictureVisitors; }
            set
            {
                if (_match.PictureVisitors != value)
                {
                    _match.PictureVisitors = value;
                    OnPropertyChanged("PictureVisitors");
                }
            }
        }

		public string Date
        {
            get
            {
				return string.Format("{0:f}",DateTime.Parse( _match.Date));
            }
            set
            {
                if (_match.Date != value)
                {
                    _match.Date = value;

                    OnPropertyChanged("Date");
                }
            }
        }

        public string Stadium
        {
            get
            {
                return $"Стадион: {_match.Stadium}";
            }
            set
            {
                if(_match.Stadium !=value)
                {
                    _match.Stadium = value;
                    OnPropertyChanged("Stadium");
                }

            }
        }



#endregion


    }
}
