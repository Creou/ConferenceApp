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
	public class BaseView : ContentPage
	{
		protected async virtual void SubscribeToMessages()
		{
			// TODO - Remove below workaround when bug 22228 is fixed
			await Task.Delay(50);

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionDetailPage.ToString(),
				(navigationMessage) =>
				{
					if (navigationMessage.Parameter != null)
					{
						Navigation.PushAsync(new SessionDetailPage((ISessionViewModel)navigationMessage.Parameter));
					}
				});

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowErrorMessage.ToString(),
				(navigationMessage) =>
				{
					if (navigationMessage.Parameter != null)
					{
						Device.BeginInvokeOnMainThread(() =>
							DisplayAlert((string)navigationMessage.Parameter2, (string)navigationMessage.Parameter, "OK"));
					}
				});

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ClosePage.ToString(),
				(navigationMessage) => Navigation.PopAsync());
		}

		protected virtual void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionDetailPage.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowErrorMessage.ToString());
			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ClosePage.ToString());
		}
	}
}
