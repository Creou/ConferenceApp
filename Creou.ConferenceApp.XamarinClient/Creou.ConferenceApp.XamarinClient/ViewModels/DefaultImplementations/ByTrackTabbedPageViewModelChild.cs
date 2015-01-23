using System.Collections.Generic;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class ByTrackTabbedPageViewModelChild : BaseViewModel
	{
		public TrackViewModel Track { get; set; }

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
	}
}
