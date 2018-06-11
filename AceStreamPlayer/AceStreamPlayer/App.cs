using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace AceStreamPlayer
{
    public class App : Application
    {
        private const string _databaseName = "AceStreamDB.db";
        private static SQLiteConnection _db = null;

        public static SQLiteConnection DataBase
        {
            get
            {
                if (_db == null)
                    _db = new SQLiteConnection(_databaseName);
                
                return _db;
            }
        }
        public App()
        {
			MainPage = new NavigationPage(new StartPage(){ Title = "Футбол. Выбор чемпионата" })
            { 
				BarBackgroundColor = Color.MediumSeaGreen,

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
