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
	public partial class ByTimePage : BaseView
	{
		public ByTimePage()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			SubscribeToMessages();
			((IByTimePageViewModel)BindingContext).OnPageAppearing();
			base.OnAppearing();
		}

		protected override void SubscribeToMessages()
		{
			base.SubscribeToMessages();

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowTimeButtons.ToString(),
				(navigationMessage) =>
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						btnEarlier.IsVisible = true;
						btnLater.IsVisible = true;
					});
				});

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.HideTimeButtons.ToString(),
				(navigationMessage) =>
				{
					Device.BeginInvokeOnMainThread(() =>
					{
						btnEarlier.IsVisible = false;
						btnLater.IsVisible = false;
					});
				});
		}

		protected override void OnDisappearing()
		{
			var viewModel = (IByTimePageViewModel)BindingContext;
			App.ByTimePageSelectedIndex = viewModel.SelectedTimeIndex;

			UnsubscribeFromMessages();
			base.OnDisappearing();
		}

		protected override void UnsubscribeFromMessages()
		{
			base.UnsubscribeFromMessages();

			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowTimeButtons.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.HideTimeButtons.ToString());
		}
	}
}
