using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace AceStreamPlayer
{
    public partial class BroadcastPage : ContentPage
    {
        public BroadcastPage(List<Reference>references, Match match)
        {
            InitializeComponent();
            BindingContext = new BroadcastViewModel(references,match);
        }



    }
}
