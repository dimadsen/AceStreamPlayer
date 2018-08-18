using System.Collections.Generic;
using SQLite;

namespace AceStreamPlayer
{
    public class Sport
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Ignore]
        public List<Championat> Championats { get; set; }
    }
}
