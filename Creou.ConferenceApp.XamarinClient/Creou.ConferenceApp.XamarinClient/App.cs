using Creou.ConferenceApp.XamarinClient.Controllers;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Creou.ConferenceApp.XamarinClient.Views;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient
{
	public class App
	{
		public static Page GetMainPage()
		{
			Locator.DataManager.GenerateAndStoreAppInstanceIdIfNotExist();

			var mainNav = new NavigationPage(new HomePage());
			return mainNav;
		}

		public static int ByTimePageSelectedIndex { get; set; }

		public static int ByTrackPageSelectedIndex { get; set; }

		private static Locator _locator;

		public static Locator Locator
		{
			get { return _locator ?? ( _locator = new Locator()); }
		}

		public static IDataManager DataManager
		{
			get { return Locator.DataManager; }
		}

		public static IHomePageViewModel HomePageViewModel
		{
			get { return Locator.HomePageViewModel; }
		}

		public static IByTimePageViewModel ByTimePageViewModel
		{
			get { return Locator.ByTimePageViewModel; }
		}

		public static IByTrackPageViewModel ByTrackPageViewModel
		{
			get { return Locator.ByTrackPageViewModel; }
		}

		public static ISessionDetailPageViewModel SessionDetailPageViewModel
		{
			get { return Locator.SessionDetailPageViewModel; }
		}

		public static ISessionFeedbackPageViewModel SessionFeedbackPageViewModel
		{
			get { return Locator.SessionFeedbackPageViewModel; }
		}

		public static IEventFeedbackPageViewModel EventFeedbackPageViewModel
		{
			get { return Locator.EventFeedbackPageViewModel; }
		}

		public static ISessionViewModel SessionViewModel
		{
			get { return Locator.SessionViewModel; }
		}

		public static ITrackViewModel TrackViewModel
		{
			get { return Locator.TrackViewModel; }
		}
	}
}
