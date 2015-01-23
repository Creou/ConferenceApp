using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public partial class SessionFeedbackPage : BaseView
	{
		private readonly ISessionViewModel _session;

		public SessionFeedbackPage(ISessionViewModel session)
		{
			_session = session;

			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			((ISessionFeedbackPageViewModel)BindingContext).Session = _session;
			((ISessionFeedbackPageViewModel)BindingContext).PopulateForm();
			SubscribeToMessages();
			base.OnAppearing();
		}

		protected override void SubscribeToMessages()
		{
			base.SubscribeToMessages();

			MessagingCenter.Subscribe<NavigationMessage>(this, Enums.eNavigationMessage.CancelFeedbackConfirmation.ToString(),
				async (navigationMessage) => ClosePageWithConfirmation());
		}

		protected override void OnDisappearing()
		{
			((ISessionFeedbackPageViewModel)BindingContext).PersistUserNameToStorage();
			UnsubscribeFromMessages();
			base.OnDisappearing();
		}

		protected override void UnsubscribeFromMessages()
		{
			base.UnsubscribeFromMessages();

			MessagingCenter.Unsubscribe<NavigationMessage>(this, Enums.eNavigationMessage.CancelFeedbackConfirmation.ToString());
		}

		protected override bool OnBackButtonPressed()
		{
			ClosePageWithConfirmation();

			return true;
		}

		private async void ClosePageWithConfirmation()
		{
			var viewModel = (ISessionFeedbackPageViewModel)BindingContext;
			
			if (viewModel.FeedbackSavedLocally || await DisplayAlert("Session Feedback", "Are you sure? All entered information will be lost.", "Yes", "No"))
			{
				await Navigation.PopAsync();
			}
		}
	}
}
