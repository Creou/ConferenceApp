using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public partial class ByTrackPage : BaseView
	{
		public ByTrackPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			SubscribeToMessages();
			((IByTrackPageViewModel)BindingContext).OnPageAppearing();
			base.OnAppearing();
		}

		protected override void SubscribeToMessages()
		{
			base.SubscribeToMessages();

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowPicker.ToString(),
				(navigationMessage) =>
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						pkrTracks.IsVisible = true;
					});
				});

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.HidePicker.ToString(),
				(navigationMessage) =>
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						pkrTracks.IsVisible = false;
					});
				});
		}

		protected override void OnDisappearing()
		{
			var viewModel = (IByTrackPageViewModel)BindingContext;
			App.ByTrackPageSelectedIndex = viewModel.SelectedTrackIndex;

			UnsubscribeFromMessages();
			base.OnDisappearing();
		}

		protected override void UnsubscribeFromMessages()
		{
			base.UnsubscribeFromMessages();

			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowPicker.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.HidePicker.ToString());
		}
	}
}
