using SQLite;
using Xamarin.Forms;

namespace AceStreamPlayer
{
    public class App : Application
    {
		private const string _databaseName = "AceStreamDB.db";

		private static DataBase _sql;
		public static DataBase Sql
        {
            get
            {
				if (_sql == null)             
                    
					_sql = new DataBase(_databaseName);           
                              
                return _sql;
            }
        }
        public App()
		{ 
            MainPage = new NavigationPage(new StartPage() { Title = "Футбол. Выбор чемпионата" })
            {
                BarBackgroundColor = Color.MediumSeaGreen
            };

        }
        
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
