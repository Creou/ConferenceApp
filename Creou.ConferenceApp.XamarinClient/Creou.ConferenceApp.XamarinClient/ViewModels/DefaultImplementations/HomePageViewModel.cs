using System.Windows.Input;
using Creou.ConferenceApp.XamarinClient.Controllers;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class HomePageViewModel : BaseViewModel, IHomePageViewModel
	{
		private readonly IDataManager _dataManager;

		public ICommand PressByTime { get; private set; }

		public ICommand PressByTrack { get; private set; }

		public ICommand PressEventFeedback { get; private set; }

		private bool _dataLoaded;

		public bool DataLoaded
		{
			get { return _dataLoaded; }
			set
			{
				_dataLoaded = value;
				OnPropertyChanged();

				if (value == true)
				{
					HideNavBar();
				}
			}
		}

		public HomePageViewModel(IDataManager dataManager)
		{
			_dataManager = dataManager;

			LoadData();

			PressByTime = new Command(NavigateToByTimePage);
			PressByTrack = new Command(NavigateToByTrackPage);
			PressEventFeedback = new Command(NavigateToEventFeedbackPage);
		}

		private async void LoadData()
		{
			IsBusy = true;

			await _dataManager.InitialDataLoad();

			switch (_dataManager.SessionDataLoadState)
			{
				case Enums.eDataLoadState.NoDataLoaded:
					IsBusy = false;
					ShowErrorMessage("Could not load data - please check your data connection and try again.");
					break;

				case Enums.eDataLoadState.DataLoadedFromLocalCache:
					IsBusy = false;
					ShowErrorMessage("No data connection - working in offline mode (this data may be out of date).", "Warning");
					DataLoaded = true;
					break;

				case Enums.eDataLoadState.DataLoadedFromServer:
					if (!_dataManager.FeedbackDataLoaded)
					{
						ShowErrorMessage("Failed to load previously entered feedback data.");
					}
					IsBusy = false;
					DataLoaded = true;
					break;
			}
		}

		private void NavigateToByTimePage()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.ShowByTimePage.ToString());
		}

		private void NavigateToByTrackPage()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.ShowByTrackPage.ToString());
		}

		private void NavigateToEventFeedbackPage()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.ShowEventFeedbackPage.ToString());
		}

		private void HideNavBar()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.HideNavBar.ToString());
		}
	}
}
