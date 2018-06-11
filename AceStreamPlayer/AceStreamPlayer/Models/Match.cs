﻿using System;
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
        public string Hosts { get; set; }
        public string Visitors { get; set; }
		public string Date { get; set; }
        public string Time { get; set; }
        public string Stadium { get; set; }
        public string PictureHosts { get; set; }
        public string PictureVisitors { get; set; }
		public string Status { get; set; }
        public int ChampionatId { get; set; }
        public Championat Championat { get; set; }
        public List<Reference> References { get; set; }
    }
}
