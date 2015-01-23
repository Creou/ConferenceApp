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
	public partial class SessionDetailPage : BaseView
	{
		private readonly ISessionViewModel _session;

		public SessionDetailPage(ISessionViewModel session)
		{
			_session = session;

			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			((ISessionDetailPageViewModel)BindingContext).Session = _session;
			SubscribeToMessages();
			base.OnAppearing();
		}

		protected override void SubscribeToMessages()
		{
			base.SubscribeToMessages();

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionFeedbackPage.ToString(),
				(navigationMessage) => Navigation.PushAsync(new SessionFeedbackPage((ISessionViewModel)navigationMessage.Parameter)));
		}

		protected override void OnDisappearing()
		{
			UnsubscribeFromMessages();
			base.OnDisappearing();
		}

		protected override void UnsubscribeFromMessages()
		{
			base.UnsubscribeFromMessages();

			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.ShowSessionFeedbackPage.ToString());
		}
	}
}
