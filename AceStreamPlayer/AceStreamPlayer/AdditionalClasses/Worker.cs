using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace AceStreamPlayer
{
	public static class Worker
	{
		public static void Start()
		{
			var championatsUrls = GetUrls();

			championatsUrls.ForEach(url =>
			{
				var championat = Parser.GetChampionat(url);
				DataBase.SaveOrUpdateChampionat(championat);
			});
		}

		private static string[] GetUrls()
        {
            var urls = new string[]
            {
                "https://livesport.ws/league/2018-fifa-world-cup"
            };
            return urls;
        }
	}
}
