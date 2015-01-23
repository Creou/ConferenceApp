using System.Windows.Input;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class SessionDetailPageViewModel : BaseViewModel, ISessionDetailPageViewModel
	{
		public ICommand PressRateSession { get; private set; }

		private ISessionViewModel _session;

		public ISessionViewModel Session
		{
			get { return _session; }
			set
			{
				_session = value;
				OnPropertyChanged();
			}
		}

		public SessionDetailPageViewModel()
		{
			PressRateSession = new Command(NavigateToSessionFeedbackPage);
		}

		private void NavigateToSessionFeedbackPage()
		{
			MessagingCenter.Send(new NavigationMessage { Parameter = Session },
				Enums.eNavigationMessage.ShowSessionFeedbackPage.ToString());
		}
	}
}
