using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer.AdditionalClasses
{
    public class ReferencesParser
    {
		public static void GetReferences(Match match)
        {
			CreateReferences(HttpWorker.GetReferencesTable(match.Url).Result, match);
        }

        private static void CreateReferences(IHtmlDocument doc, Match match)
        {
            match.References = new List<Reference>();

			try
			{
				var trs = doc.QuerySelectorAll("li").Where(x => x.Attributes.Any(y => y.Value == "3")).First()
                 .QuerySelectorAll("tbody").First().QuerySelectorAll("tr").ToList();

                trs.ForEach(tr =>
                {
                    match.References.Add(CreateReference(tr, match.Id));
                });
			}
			catch
			{
				
			}
            

        }

        

		private static Reference CreateReference(IElement tr, int matchId)
        {
            var reference = new Reference();

			reference.MatchId = matchId;

            var tds = tr.QuerySelectorAll("td").ToList();

            for (int td = 1; td < tds.Count; td++)
            {
                if (td == 1)
                {
                    reference.LanguagePicture = tds[1].QuerySelector("img").Attributes.Where(x => x.Name == "src").First().Value;
                }
                else if (td == 2)
                {
                    reference.Quality = tds[2].TextContent;
                }
                else if (td == 3)
                {
                    reference.Channel = tds[3].TextContent;
                }
                else if (td == 4)
                {
                    reference.Fps = tds[4].TextContent;
                }
                else if (td == 5)
                {
                    reference.Format = tds[5].TextContent;
                }
                else if (td == 6)
                {
                    reference.ContentId = tds[6].QuerySelector("a").Attributes.Where(x => x.Name == "href")
                        .First().Value.Replace("acestream://", string.Empty);
                }

            }
            return reference;
        }
    }
}
