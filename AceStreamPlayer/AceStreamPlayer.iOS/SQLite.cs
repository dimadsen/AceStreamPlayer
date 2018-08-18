using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(AceStreamPlayer.iOS.SQLite))]
namespace AceStreamPlayer.iOS
{
    public class SQLite : ISQLite
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);

            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }

            return path;
        }
    }
}
