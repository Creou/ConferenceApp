using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.Models;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public class SessionsByTrackViewModel : BaseViewModel
	{
		private TrackViewModel _track;

		public TrackViewModel Track
		{
			get { return _track; }
			set
			{
				_track = value;

				Task.Run(async () =>
				{
					IsBusy = true;
					Sessions = await _dataManager.GetSessionsByTrackAsync(Track);
					Heading = "Sessions on " + value.Name + ":";
					IsBusy = false;
				});

				OnPropertyChanged();
			}
		}

		private IEnumerable<SessionViewModel> _sessions;

		public IEnumerable<SessionViewModel> Sessions
		{
			get { return _sessions; }
			set
			{
				_sessions = value;
				OnPropertyChanged();
			}
		}

		private DataManager _dataManager;

		public SessionsByTrackViewModel()
		{
			_dataManager = App.Data;
		}
	}
}