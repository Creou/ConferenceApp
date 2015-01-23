using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.Controllers;
using Creou.ConferenceApp.XamarinClient.Controllers.DefaultImplementations;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class ByTrackPageViewModel : BaseViewModel, IByTrackPageViewModel
	{
		private IDataManager _dataManager;

		private List<ITrackViewModel> _availableTracks;

		public List<ITrackViewModel> AvailableTracks
		{
			get { return _availableTracks; }
			set
			{
				_availableTracks = value;
				OnPropertyChanged();

				SelectedTrackIndex = App.ByTrackPageSelectedIndex;
				SelectedTrack = AvailableTracks[App.ByTrackPageSelectedIndex];
			}
		}

		private int _selectedTrackIndex;

		public int SelectedTrackIndex
		{
			get { return _selectedTrackIndex; }
			set
			{
				_selectedTrackIndex = value;

				SelectedTrack = AvailableTracks[value];
			}
		}

		private ITrackViewModel _selectedTrack;

		public ITrackViewModel SelectedTrack
		{
			get { return _selectedTrack; }
			set
			{
				_selectedTrack = value;
				OnPropertyChanged();

				Task.Run(async () =>
					Sessions = await _dataManager.GetSessionsByTrackAsync(SelectedTrack));
			}
		}

		private IEnumerable<ISessionViewModel> _sessions;

		public IEnumerable<ISessionViewModel> Sessions
		{
			get { return _sessions; }
			set
			{
				_sessions = value;
				OnPropertyChanged();
				ShowPicker();
			}
		}

		public ByTrackPageViewModel(IDataManager dataManager)
		{
			_dataManager = dataManager;

			LoadData();
		}
		
		private void LoadData()
		{
			Task.Run(() =>
			{
				IsBusy = true;
				AvailableTracks = _dataManager.GetAvailableTracksAsync().Result.ToList();
				IsBusy = false;
			});
		}

		public void OnPageAppearing()
		{
			if (AvailableTracks != null)
			{
				ShowPicker();
			}
		}

		private void ShowPicker()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.ShowPicker.ToString());
		}

		private void HidePicker()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.HidePicker.ToString());
		}
	}
}
