using System;
using System.IO;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AceStreamPlayer.iOS.SQLite))]
namespace AceStreamPlayer.iOS
{
    public class SQLite: ISQLite
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            // определяем путь к бд
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // папка библиотеки
            var path = Path.Combine(libraryPath, sqliteFilename);

            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }


            return path;


        }
    }
}
