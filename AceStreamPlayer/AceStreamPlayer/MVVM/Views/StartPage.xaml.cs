using Xamarin.Forms;

namespace AceStreamPlayer
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            BindingContext = new StartViewModel()
            {
                Navigation = Navigation
            };
        }
    }
}
