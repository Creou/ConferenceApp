using System;
using System.Windows.Input;
using Creou.ConferenceApp.XamarinClient.Models;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class SessionViewModel : BaseViewModel, ISessionViewModel
	{
		public Session Session { get; set; }

		public string SessionTitleUpper { get; set; }

		public string SessionTitleShort { get; set; }

		public string TimesString { get; set; }

		public string TimesRoomAndTrackString { get; set; }

		public ICommand SessionSelected { get; private set; }

		public SessionViewModel()
		{
			SessionSelected = new Command<ISessionViewModel>(ShowSessionDetail);
		}

		private void ShowSessionDetail(ISessionViewModel session)
		{
			MessagingCenter.Send(new NavigationMessage { Parameter = session },
				Enums.eNavigationMessage.ShowSessionDetailPage.ToString());
		}

		public bool SessionFinished
		{
			get { return (DateTime.Now > Session.End); }
		}
	}
}