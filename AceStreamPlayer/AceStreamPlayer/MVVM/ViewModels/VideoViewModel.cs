using FormsVideoLibrary;

namespace AceStreamPlayer
{
	public class VideoViewModel : BaseViewModel
	{

		public VideoViewModel(Reference reference, VideoPlayer videoPlayer)
		{
			WatchMatch(videoPlayer, reference);
		}


		private async void WatchMatch(VideoPlayer player, Reference reference)
		{
			var uri = $"http://192.168.1.37:6878/ace/manifest.m3u8?format=json&id={reference.ContentId}";

			var url = await GetUrl<Response>(reference.ContentId);
			player.Source = new UriVideoSource
			{
				Uri = url?.playback_url
			};
		}

	}
}
