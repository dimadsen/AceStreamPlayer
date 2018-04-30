using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace AceStreamPlayer
{
    public class BroadcastViewModel:BaseViewModel
    {
        private Match _match;

        public BroadcastViewModel(List<Reference>references, Match match)
        {
            References = new ObservableCollection<Reference>(references);
            this._match = match;
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
                if(_match.Visitors != null)
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

         

    }
}
