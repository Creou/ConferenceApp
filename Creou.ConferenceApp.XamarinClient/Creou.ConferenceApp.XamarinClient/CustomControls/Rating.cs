using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.CustomControls
{
	public class Rating : Slider
	{
		public Rating()
		{
			Minimum = 0;
			Maximum = 5;

			var currentHeight = Height;

			HeightRequest = Device.OnPlatform(
				iOS: currentHeight,
				Android: currentHeight,
				WinPhone: 80);
		}
	}
}
