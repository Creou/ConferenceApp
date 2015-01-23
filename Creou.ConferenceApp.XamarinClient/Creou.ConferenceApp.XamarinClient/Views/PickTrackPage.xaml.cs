using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public partial class PickTrackPage : BaseView
	{	
		public PickTrackPage()
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

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionsByTrackPage.ToString(),
				(navigationMessage) =>
				{
					try
					{
						// Perform the actual navigation here
						if (navigationMessage.Parameter != null)
						{
							Navigation.PushAsync(new SessionsByTrackPage((TrackViewModel)navigationMessage.Parameter));
						}
						//else
						//{
						//	Navigation.PushAsync(new SessionsByTrackPage());
						//}
					}
					catch (Exception ex)
					{
						// TODO
					}
				});
		}

		protected override void OnDisappearing()
		{
			UnsubscribeFromMessages();
			base.OnDisappearing();
		}

		private void UnsubscribeFromMessages()
		{
			base.UnsubscribeFromMessages();

			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionsByTrackPage.ToString());
		}
	}
}
