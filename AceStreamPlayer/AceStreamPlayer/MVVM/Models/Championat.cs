using System.Collections.Generic;
using SQLite;

namespace AceStreamPlayer
{
    public class Championat
    {
		[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tour { get; set; }
        public string Picture { get; set; }
        [Ignore]
        public Sport Sport { get; set; }
        public int SportId { get; set; }
		[Ignore]
        public List<Match> Matches { get; set; }
    }
}
