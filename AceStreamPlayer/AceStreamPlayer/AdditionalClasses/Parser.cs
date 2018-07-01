using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
	public static class Parser
	{
		public static Championat GetChampionat(string champUrl)
		{
			var matchesUrls = GetMatchesUrls(GetDocument(champUrl).Result);

			var championat = CreateChampionat(GetDocument(champUrl).Result);

			matchesUrls.ForEach(m =>
		   {
			   championat.Matches.Add(CreateMatch(GetDocument(m).Result, championat));
		   });

			return championat;
		}

		private static Match CreateMatch(IDocument document, Championat championat)
		{
			var match = new Match();
			GetCommand(document, match);
			GetInfo(document, match, championat);

			return match;
		}
		private static Championat CreateChampionat(IDocument document)
		{
			var championat = new Championat();

			championat.Name = document.QuerySelectorAll("section").Where(x => x.ClassName == "general-info").First().QuerySelectorAll("div").Where(x => x.ClassName == "title-holder").First().
				QuerySelectorAll("h1").Where(x => x.ClassName == "name").First().TextContent;

			championat.Matches = new List<Match>();

			return championat;
		}



		private static void GetCommand(IDocument document, Match match)
		{
			var commands = document.QuerySelectorAll("section").Where(x => x.ClassName == "info-holder").First().
							   QuerySelectorAll("div").Where(x => x.ClassName == "command").ToList();

			for (int i = 1; i < commands.Count; i++)
			{
				if (i == 1)
					match.Hosts = $"{GetCommandName(commands[0], true, true)} {GetCommandName(commands[0], false, true)}";
				else if (i == 2)
					match.Visitors = $"{GetCommandName(commands[1], true, false)} {GetCommandName(commands[1], false, false)}";
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

		private static string GetCommandName(IElement element, bool isName, bool isHost)
		{
			string name = string.Empty;

			if (isName && isHost)
				name = element.QuerySelectorAll("div").Where(x => x.ClassName == "name").First().QuerySelector("a").TextContent;
			else if (isName && !isHost)
				name = element.QuerySelectorAll("div").Where(x => x.ClassName == "name").ToList().Last().QuerySelector("a").TextContent;
			else
				name = element.QuerySelectorAll("div").Where(x => x.ClassName == "country").First().TextContent;

			return name;
		}

		private static async Task<IDocument> GetDocument(string url)
		{
			var config = Configuration.Default.WithDefaultLoader();
			var document = BrowsingContext.New(config).OpenAsync(url);
			return document.Result;
		}

		private static List<string> GetMatchesUrls(IDocument document)
		{
			var matchesUrls = new List<string>();

			var hrefs = document.QuerySelectorAll("a").Where(x => x.ClassName == "dark-btn").ToList();
			hrefs.ForEach(href => matchesUrls.Add((string)href.GetType().GetProperty("Href").GetValue(href)));

			return matchesUrls;
		}

	}
}
