using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public partial class HomePage : BaseView
	{
		public HomePage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			SubscribeToMessages();
			base.OnAppearing();
		}

		protected override void SubscribeToMessages()
		{
			base.SubscribeToMessages();

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowByTimePage.ToString(),
				(navigationMessage) => Navigation.PushAsync(new ByTimePage()));

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowByTrackPage.ToString(),
				(navigationMessage) => Navigation.PushAsync(new ByTrackPage())); //Navigation.PushAsync(new ByTrackTabbedPage());

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowEventFeedbackPage.ToString(),
				(navigationMessage) => Navigation.PushAsync(new EventFeedbackPage()));

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.HideNavBar.ToString(),
				(navigationMessage) => NavigationPage.SetHasNavigationBar(this, false));
		}

		protected override void OnDisappearing()
		{
			UnsubscribeFromMessages();
			base.OnDisappearing();
		}

		protected override void UnsubscribeFromMessages()
		{
			base.UnsubscribeFromMessages();

			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowByTimePage.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowByTrackPage.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowEventFeedbackPage.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.HideNavBar.ToString());
		}
	}
}
