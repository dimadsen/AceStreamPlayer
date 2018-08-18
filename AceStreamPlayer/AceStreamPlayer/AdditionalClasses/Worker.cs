using AceStreamPlayer.AdditionalClasses;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
    public static class Worker
    {
        public static void StartEventParse()
        {
            var championatsUrls = GetUrls();

            championatsUrls.ForEach(url =>
            {
                var championat = EventParser.GetChampionat(url);
                App.Sql.SaveOrUpdateChampionat(championat);
            });
        }

        private static string[] GetUrls()
        {
            var urls = new string[]
            {
                "https://livesport.ws/league/russia-premier-league",
                "https://livesport.ws/league/england-premier-league",
            };
            return urls;
        }

        public static void StartReferencesParse(Match match)
        {
            ReferencesParser.GetReferences(match);
            match.References.ForEach(reference =>
            {
                App.Sql.SaveReference(reference);
            });
        }
    }
}
