using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using AceStreamPlayer.AdditionalClasses;
using Xamarin.Forms;

namespace AceStreamPlayer
{
	public class BroadcastViewModel : BaseViewModel
	{
		private Match _match;
		private List<Reference> _references;

		public BroadcastViewModel(List<Reference> references, Match match)
		{
			References = new ObservableCollection<Reference>(references);
			_match = match;
			_references = references;

			refreshCommand = new Command(() =>
            {
                IsBusy = true;
				References = RefreshList();
                IsBusy = false;

            });
		}

		public ObservableCollection<Reference> References { get; set; }

		private Reference selectedReference;
		public Reference SelectedReference
		{
			get { return selectedReference; }
			set
			{
				if (value != null)
				{
					var reference = value;
					selectedReference = null;

					OnPropertyChanged(nameof(SelectedReference));
					WatchMatch(reference);
				}
			}
		}
		private void WatchMatch(Reference reference)
		{
			Navigation.PushAsync(new VideoPage(reference));
		}

		private ObservableCollection<Reference> RefreshList()
        {
            IsRefreshing = true;
			Worker.StartReferencesParse(_match);
            IsRefreshing = false;
			return App.Sql.GetReferences(_match);
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
		public string HostsCountry
        {
			get { return _match.HostsCountry; }
            set
            {

				if (_match.HostsCountry != value)
                {
					_match.HostsCountry = value;
					OnPropertyChanged("HostsCountry");
                }

            }
        }

		public string VisitorsCountry
        {
			get { return _match.VisitorsCountry; }
            set
            {
				if (_match.VisitorsCountry != null)
                {
					_match.VisitorsCountry = value;
					OnPropertyChanged("VisitorsCountry");
                }

            }
        }


		public string PictureHosts
		{
			get { return _match.PictureHosts; }
			set
			{
				if(_match.PictureHosts != value)
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
			get { return _match.Date; }
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
				if (_match.Stadium != value)
				{
					_match.Stadium = value;
					OnPropertyChanged("Stadium");
				}

			}
		}
		#endregion



	}


    
}
