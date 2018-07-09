using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQLite;
using Xamarin.Forms.Internals;


namespace AceStreamPlayer
{
	public static class DataBase
	{
		private const string _databaseName = "AceStreamDB.db";
		private static SQLiteConnection _db = null;

		public static SQLiteConnection Sql
		{
			get
			{
				if (_db == null)
					_db = new SQLiteConnection(_databaseName);
                
				return _db;
			}
		}



		private static void SaveOrUpdateMatch(Match match)
		{
			var existingMatch = Sql.Table<Match>().Where(x => x.Hosts == match.Hosts && x.Visitors == match.Visitors && x.Date == match.Date).FirstOrDefault();

			if (existingMatch == null)
				Sql.Insert(match);

			else
			{
				match.Id = existingMatch.Id;
				Sql.Update(match);
			}
		}

		public static void SaveOrUpdateChampionat(Championat championat)
		{
			var existingChamp = Sql.Table<Championat>().Where(x => x.Name == championat.Name).FirstOrDefault();

			if (existingChamp == null)
				Sql.Insert(championat);
			else
			{
				championat.Id = existingChamp.Id;
				championat.Matches.ForEach(m =>
				{
					m.ChampionatId = championat.Id;
					SaveOrUpdateMatch(m);
				});
			}

		}

  
		public static ObservableCollection<T> GetCollection<T>() where T : new()
		{

			var list = Sql.Table<T>().ToList();
			var collection = new ObservableCollection<T>(list);

			return collection;
		}
	}
}
