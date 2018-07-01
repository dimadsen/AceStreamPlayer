using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
	public static class Worker
	{
		public static void Start(List<string> championatsUrls)
		{
			championatsUrls.ForEach(url =>
			{
				var championat = Parser.GetChampionat(url);
				DataBase.SaveOrUpdateChampionat(championat);
			});
		}

		public static List<string> GetUrls()
        {
            var urls = new List<string>()
            {
                "https://livesport.ws/league/2018-fifa-world-cup"
            };
            return urls;
        }
	}
}
