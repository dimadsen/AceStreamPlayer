using System;
namespace AceStreamPlayer
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
