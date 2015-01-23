using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public class HomePage_OLD : ContentPage
	{
		private DataManager _dataManager;

		public HomePage_OLD()
		{
			_dataManager = App.Data;

			BuildPage();
		}

		private async void BuildPage()
		{
			Padding = new Thickness(20);

			//BackgroundColor = Color.White;

			var heading = new Label
			{
				Text = "CONFERENCE APP",
				BackgroundColor = Color.Accent,
				Font = Font.SystemFontOfSize(18)
			};

			var logo = new Image
			{
				Aspect = Aspect.AspectFit
			};
			logo.Source = ImageSource.FromFile("LogoRectangle.png");

			var byTimeSlot = new Button
			{
				//TextColor = Color.Black,
				//BorderColor = Color.Black,
				//BackgroundColor = Color.White,
				
				Text = "View sessions by time slot",
				Font = Font.SystemFontOfSize(24),
				HorizontalOptions = LayoutOptions.Center
			};
			byTimeSlot.Clicked += byTimeSlot_Clicked;

			var byTrack = new Button
			{
				//TextColor = Color.Black,
				//BorderColor = Color.Black,
				//BackgroundColor = Color.White,

				Text = "View sessions by track",
				Font = Font.SystemFontOfSize(24),
				HorizontalOptions = LayoutOptions.Center
			};
			byTrack.Clicked += byTrack_Clicked;

			Content = new StackLayout
			{
				//VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = 40,
				Children = { heading, logo, byTimeSlot, byTrack }
			};
		}

		private void byTimeSlot_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new PickTimeSlotPage(_dataManager));
		}

		private void byTrack_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new PickTrackPage());
		}

	}
}
