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
	public class BaseTabbedView : TabbedPage
	{
		protected virtual void SubscribeToMessages()
		{
			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionDetailPage.ToString(),
				(navigationMessage) =>
				{
					if (navigationMessage.Parameter != null)
					{
						try
						{
							Navigation.PushAsync(new SessionDetailPage((SessionViewModel)navigationMessage.Parameter));
						}
						catch (Exception ex)
						{
							// TODO
						}
					}
				});

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowErrorMessage.ToString(),
				(navigationMessage) =>
				{
					if (navigationMessage.Parameter != null)
					{
						Device.BeginInvokeOnMainThread(() =>
						{
							DisplayAlert((string)navigationMessage.Parameter2, (string)navigationMessage.Parameter, "OK", "Cancel");
						});
					}
				});
		}

		protected virtual void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionDetailPage.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowErrorMessage.ToString());
		}
	}
}
