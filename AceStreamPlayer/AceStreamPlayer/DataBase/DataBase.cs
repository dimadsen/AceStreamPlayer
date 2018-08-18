using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Internals;



namespace AceStreamPlayer
{
    public class DataBase
    {

        private SQLiteConnection database;

        public DataBase(string filename)
        {

            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
        }


        private void SaveOrUpdateMatch(Match match, List<Match> matches)
        {
            var existingMatch = database.Table<Match>().Where(x => x.Hosts == match.Hosts && x.Visitors == match.Visitors && x.Date == match.Date).FirstOrDefault();

            if (existingMatch == null)
                database.Insert(match);

            else
            {
                match.Id = existingMatch.Id;
                database.Update(match);
            }

            //CompleteMatches(matches);
        }


        public void SaveOrUpdateChampionat(Championat championat)
        {
            var existingChamp = database.Table<Championat>().Where(x => x.Name == championat.Name).FirstOrDefault();

            if (existingChamp == null)
            {
                database.Insert(championat);
                existingChamp = database.Table<Championat>().Where(x => x.Name == championat.Name).FirstOrDefault();
                championat.Matches.ForEach(m =>
                {
                    m.ChampionatId = existingChamp.Id;
                    SaveOrUpdateMatch(m, championat.Matches);
                });
            }

            else
            {
                championat.Id = existingChamp.Id;
                existingChamp.Tour = championat.Tour;

                database.Update(existingChamp);

                championat.Matches.ForEach(m =>
                {
                    m.ChampionatId = championat.Id;
                    SaveOrUpdateMatch(m, championat.Matches);
                });
            }

        }

        public void SaveReference(Reference reference)
        {
            var existingRef = database.Table<Reference>().Where(r => r.MatchId == reference.MatchId && r.ContentId == reference.ContentId).FirstOrDefault();

            if (existingRef == null)
                database.Insert(reference);
        }

        public ObservableCollection<T> GetCollection<T>() where T : new()
        {

            var list = database.Table<T>().ToList();
            var collection = new ObservableCollection<T>(list);

            return collection;
        }

        public ObservableCollection<Reference> GetReferences(Match match)
        {
            var list = database.Table<Reference>().Where(r => r.MatchId == match.Id).ToList();
            var collection = new ObservableCollection<Reference>(list);

            return collection;
        }


        private void CompleteMatches(List<Match> matches)
        {
            var databaseMatches = database.Table<Match>().ToList().Where(m => m.ChampionatId == matches.First().ChampionatId && !matches.Contains(m)).ToList();

            databaseMatches.ForEach(m =>
            {
                m.Status = "Завершен";
                database.Update(m);
            });

        }
        private static DateTime DateParse(string matchDate)
        {
            var date = DateTime.ParseExact(matchDate, "dd MMMM yyyy, HH:mm", new CultureInfo("ru-RU"));
            return date;
        }


        //private string GetPicture()
        //{
        //	var u = new UriImageSource() { Uri = new Uri("https://static.livesport.ws/images/logo/country/croatia.png") };

        //}

    }
}
