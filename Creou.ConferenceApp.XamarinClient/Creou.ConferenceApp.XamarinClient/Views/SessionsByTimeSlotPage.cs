using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public class SessionsByTimeSlotPage : ContentPage
	{
		private DataManager _dataManager;

		private SlotTimeViewModel _slotTime;

		public SessionsByTimeSlotPage(DataManager dataManager, SlotTimeViewModel slotTime)
		{
			_dataManager = dataManager;
			_slotTime = slotTime;

			BuildPage();
		}

		private async void BuildPage()
		{
			var viewModel = await _dataManager.GetSessionsByTimeSlotViewModelAsync(_slotTime);

			Padding = new Thickness(20);

			var heading = new Label
			{
				Text = "CONFERENCE APP",
				BackgroundColor = Color.Accent,
				Font = Font.SystemFontOfSize(18)
			};

			var subHeading = new Label
			{
				Text = viewModel.Heading,
				Font = Font.SystemFontOfSize(24)
			};
			
			var sessionListView = new ListView
			{
				RowHeight = 130,
				ItemsSource = viewModel.Sessions,
				ItemTemplate = new DataTemplate(typeof(SessionViewCell))
			};
			sessionListView.ItemSelected += timeSlotListView_ItemSelected;

			Content = new StackLayout
			{
				Spacing = 50,
				Children = { heading, subHeading, sessionListView }
			};
		}

		private async void timeSlotListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var slotTime = (SlotTimeViewModel)e.SelectedItem;
			await Navigation.PushAsync(new SessionsByTimeSlotPage(_dataManager, slotTime));
		}
	}
}
