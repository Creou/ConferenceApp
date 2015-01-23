using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public class SessionViewCell : ViewCell
	{
		public SessionViewCell()
		{
			var titleLabel = new Label
			{
				Font = Font.SystemFontOfSize(22)
			};
			titleLabel.SetBinding(Label.TextProperty, "SessionTitleUpper");

			var descLabel = new Label
			{
				Font = Font.SystemFontOfSize(18)
			};
			descLabel.SetBinding(Label.TextProperty, "Session.Description");

			var timeAndRoomLabel = new Label
			{
				Font = Font.SystemFontOfSize(18)
			};
			timeAndRoomLabel.SetBinding(Label.TextProperty, "TimesAndRoomString");
			
			var layout = new StackLayout
			{
				Spacing = 7,
				Children = { titleLabel, descLabel, timeAndRoomLabel }
			};

			View = layout;
		}
	}
}
