using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.CustomControls
{
	public class RatingTest : View
	{
		public static readonly BindableProperty ValueProperty =
			BindableProperty.Create<Rating, double>(p => p.Value, 0);

		public double Value
		{
			get { return (double)base.GetValue(ValueProperty); }
			set { base.SetValue(ValueProperty, value); }
		}

		public RatingTest()
		{
			//Minimum = 0;
			//Maximum = 5;


			//var currentHeight = Height;

			//HeightRequest = Device.OnPlatform(
			//	iOS: currentHeight,
			//	Android: currentHeight,
			//	WinPhone: 80);
 

			//WidthRequest = 20;
		}
	}
}
