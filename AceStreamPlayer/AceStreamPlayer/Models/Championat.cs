using System.Collections.Generic;

namespace AceStreamPlayer
{
    public class Championat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tour { get; set; }
        public string Picture { get; set; }
        public Sport Sport { get; set; }
        public int SportId { get; set; }
        public List<Match> Matches { get; set; }
    }
}
