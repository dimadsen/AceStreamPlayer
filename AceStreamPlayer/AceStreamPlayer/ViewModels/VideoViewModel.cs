using System;
namespace AceStreamPlayer
{
	public class VideoViewModel : BaseViewModel
    {
		private Reference _reference;

		public VideoViewModel(Reference reference)
        {
			_reference = reference;
			Url = "http://192.168.1.37:6878/hls/r/adbab9853095691096a6e4faa0aec963c3d19e82/189e5d61a3b7bc4fb60b796671d439ef.m3u8";
        }

        
        public string Url
		{
			get
			{
				_reference.ContentId = "http://192.168.1.37:6878/hls/r/adbab9853095691096a6e4faa0aec963c3d19e82/189e5d61a3b7bc4fb60b796671d439ef.m3u8";
				return _reference.ContentId;
			}
			set
			{
				//if (_reference.ContentId != value)
				//           {
				//_reference.ContentId = value;
				//OnPropertyChanged("Url");
				//}
				value = "http://192.168.1.37:6878/hls/r/adbab9853095691096a6e4faa0aec963c3d19e82/189e5d61a3b7bc4fb60b796671d439ef.m3u8";
									_reference.ContentId = "http://192.168.1.37:6878/hls/r/adbab9853095691096a6e4faa0aec963c3d19e82/189e5d61a3b7bc4fb60b796671d439ef.m3u8";
				OnPropertyChanged("Url");
			}
		}



    }
}
