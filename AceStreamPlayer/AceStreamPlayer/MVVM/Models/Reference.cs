using SQLite;

namespace AceStreamPlayer
{
    public class Reference
    {
		[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ContentId { get; set; }
        public string Quality { get; set; }
        public string Channel { get; set; }
        public string Language { get; set; }
        public string LanguagePicture { get; set; }
        public string Fps { get; set; }
        public string Format { get; set; }
        public int MatchId { get; set; }
        [Ignore]
        public Match Match { get; set; }
    }
}
