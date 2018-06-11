using System.Collections.Generic;

namespace AceStreamPlayer
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Championat> Championats { get; set; }
    }
}
