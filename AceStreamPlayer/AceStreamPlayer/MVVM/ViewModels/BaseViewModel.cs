using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;


		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion


		#region Properties

		protected bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged(nameof(IsBusy));
			}
		}  		protected bool _isRefreshing;
		public bool IsRefreshing
		{
			get { return _isRefreshing; }
			set
			{
				_isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}

		public INavigation Navigation { get; set; }

		#endregion 
		#region Commands
		protected Command refreshCommand;
		public Command RefreshCommand
		{
			get
			{
				return refreshCommand;
			}
		}
		#endregion
	}
}
