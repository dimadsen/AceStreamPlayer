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



        public string Name
        {
            get { return _match.Name; }
            set
            {
                
                if (_match.Name != value)
                {
                    _match.Name = value;
                    OnPropertyChanged("Name");
                }
                   
            }
        }

    }
}
