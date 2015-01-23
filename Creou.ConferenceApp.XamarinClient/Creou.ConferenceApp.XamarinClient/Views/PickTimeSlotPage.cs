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
	public class PickTimeSlotPage : ContentPage
	{
		private DataManager _dataManager;

		public PickTimeSlotPage(DataManager dataManager)
		{
			_dataManager = dataManager;

			BuildPage();
		}

		private async void BuildPage()
		{
			Padding = new Thickness(20);

			var heading = new Label
			{
				Text = "CONFERENCE APP",
				BackgroundColor = Color.Accent,
				Font = Font.SystemFontOfSize(18)
			};

			var subHeading = new Label
			{
				Text = "Please pick a time slot:",
				Font = Font.SystemFontOfSize(24)
			};

			IsBusy = true;
			// await Task.Delay(2000);  // TEMP
			var viewModel = await _dataManager.GetPickTimeSlotViewModelAsync();
			IsBusy = false;

			var timeSlotListView = new ListView
			{
				RowHeight = 60,
				ItemsSource = viewModel.Times,
				ItemTemplate = new DataTemplate(typeof(TextCell))
			};
			timeSlotListView.ItemTemplate.SetBinding(TextCell.TextProperty, "sTime");
			timeSlotListView.ItemSelected += timeSlotListView_ItemSelected;

			Content = new StackLayout
			{
				Spacing = 50,
				Children = { heading, subHeading, timeSlotListView }
			};
		}

		private async void timeSlotListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var slotTime = (SlotTimeViewModel)e.SelectedItem;
			await Navigation.PushAsync(new SessionsByTimeSlotPage(_dataManager, slotTime));
		}
	}
}
