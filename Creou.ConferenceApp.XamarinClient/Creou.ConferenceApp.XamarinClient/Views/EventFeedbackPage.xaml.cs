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
	public partial class EventFeedbackPage : BaseView
	{
		public EventFeedbackPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			((IEventFeedbackPageViewModel)BindingContext).PopulateForm();
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
			((IEventFeedbackPageViewModel)BindingContext).PersistUserNameToStorage();
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
			var viewModel = (IEventFeedbackPageViewModel)BindingContext;

			if (viewModel.FeedbackSavedLocally || await DisplayAlert("Event Feedback", "Are you sure? All entered information will be lost.", "Yes", "No"))
			{
				await Navigation.PopAsync();
			}
		}
	}
}
