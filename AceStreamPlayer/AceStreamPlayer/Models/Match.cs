using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceStreamPlayer
{
    public class Match
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Score { get; set; }
        public string Time { get; set; }
        public string Stadium { get; set; }
        public string PictureHome { get; set; }
        public string PictureVisitors { get; set; }
        public List<Reference> References { get; set; }
    }
}
