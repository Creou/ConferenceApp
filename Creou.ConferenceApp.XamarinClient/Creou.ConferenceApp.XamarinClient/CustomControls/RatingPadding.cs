using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.CustomControls
{
	public class RatingPadding : BoxView
	{
		public RatingPadding()
		{
			HeightRequest = Device.OnPlatform(
				iOS: 0,
				Android: 0,
				WinPhone: 20);
		}
	}
}
