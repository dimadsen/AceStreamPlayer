using System.Collections.Generic;
using SQLite;

namespace AceStreamPlayer
{
    public class Match
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Ignore]
        public string Name { get; set; }
        public string Hosts { get; set; }
        public string HostsCountry { get; set; }
        public string Visitors { get; set; }
        public string VisitorsCountry { get; set; }
        public string Date { get; set; }
        [Ignore]
        public string Time { get; set; }
        public string Stadium { get; set; }
        public string PictureHosts { get; set; }
        public string PictureVisitors { get; set; }
        public string Status { get; set; }
        public int ChampionatId { get; set; }
        public string Url { get; set; }
        [Ignore]
        public Championat Championat { get; set; }
        [Ignore]
        public List<Reference> References { get; set; }
    }
}
