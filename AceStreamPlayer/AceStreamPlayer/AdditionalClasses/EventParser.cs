using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AceStreamPlayer.AdditionalClasses;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
	public static class EventParser
	{
		public static Championat GetChampionat(string champUrl)
		{
			var championat = CreateChampionat(GetDocument(champUrl).Result);

			var matchesUrls = GetMatchesUrls(GetDocument(champUrl).Result);

			matchesUrls.ForEach(matchUrl =>
		   {
			   championat.Matches.Add(CreateMatch(GetDocument(matchUrl).Result, championat));
		   });

			return championat;
		}

		private static async Task<IDocument> GetDocument(string url)
		{
			var config = Configuration.Default.WithDefaultLoader();
			var document = BrowsingContext.New(config).OpenAsync(url);
			return document.Result;
		}

		private static Championat CreateChampionat(IDocument document)
		{
			var championat = new Championat();

			championat.SportId = 1;
			championat.Name = document.QuerySelectorAll("section").Where(x => x.ClassName == "general-info").First().QuerySelectorAll("div").Where(x => x.ClassName == "title-holder").First().
				QuerySelectorAll("h1").Where(x => x.ClassName == "name").First().TextContent;

			championat.Matches = new List<Match>();

			return championat;
		}

		private static List<string> GetMatchesUrls(IDocument document)
		{
			var matchesUrls = new List<string>();

			var hrefs = document.QuerySelectorAll("a").Where(x => x.ClassName == "dark-btn").ToList();
			hrefs.ForEach(href => matchesUrls.Add((string)href.GetType().GetProperty("Href").GetValue(href)));

			return matchesUrls;
		}

		private static Match CreateMatch(IDocument document, Championat championat)
		{
			var match = new Match();

			GetCommand(document, match);
			GetInfo(document, match, championat);
			ShapingUrlForHttpRequest(document, match);

			return match;
		}

		#region For match
		private static void GetCommand(IDocument document, Match match)
		{
			var commands = document.QuerySelectorAll("section").Where(x => x.ClassName == "info-holder").First().
							   QuerySelectorAll("div").Where(x => x.ClassName == "command").ToList();

			for (int i = 1; i <= commands.Count; i++)
			{
				if (i == 1)
				{
					match.Hosts = GetCommandName(commands[0], true);
					match.HostsCountry = GetCommandCountry(commands[0]);
				}

				else if (i == 2)
				{
					match.Visitors = GetCommandName(commands[1], false);
					match.VisitorsCountry = GetCommandCountry(commands[1]);
				}
			}
		}

		private static void GetInfo(IDocument document, Match match, Championat championat)
		{
			var info = document.QuerySelectorAll("div").Where(x => x.ClassName == "info").First();

			match.Status = info.QuerySelectorAll("div").Where(x => x.ClassName == "broadcasting").First().QuerySelector("span").TextContent;

			match.Date = info.QuerySelectorAll("div").Where(x => x.ClassName == "date set-offset-datetime").First().TextContent;

			match.Stadium = info.QuerySelectorAll("div").Where(x => x.ClassName == "definition").First().
							  QuerySelectorAll("span").Where(x => x.ClassName == "dd text").First().TextContent;

			championat.Tour = document.QuerySelectorAll("span").Where(x => x.ClassName == "tour").First().TextContent;
		}

		private static string GetCommandName(IElement element, bool isHost)
		{
			string name = string.Empty;

			if (isHost)
				name = element.QuerySelectorAll("div").Where(x => x.ClassName == "name").First().QuerySelector("a").TextContent;
			else
				name = element.QuerySelectorAll("div").Where(x => x.ClassName == "name").ToList().Last().QuerySelector("a").TextContent;


			return name;
		}

		private static string GetCommandCountry(IElement element)
		{
			var country = element.QuerySelectorAll("div").Where(x => x.ClassName == "country").First().TextContent;
			return country;
		}

		private static void ShapingUrlForHttpRequest(IDocument document, Match match)
        {
            var eventId = document.QuerySelectorAll("div").Where(x => x.ClassName == "home-staff").First().Attributes.
                                  Where(x => x.Name == "event_id").First().Value;

            var dataId = document.QuerySelectorAll("div").Where(x => x.ClassName == "template aside").First().Attributes.
                                 Where(x => x.Name == "data-id").First().Value;

			match.Url = $"https://livesport.ws/engine/modules/sports/sport_refresh.php?from=event&event_id={eventId}&tab_id=undefined&post_id={dataId}";

        }
		#endregion



	}
}
