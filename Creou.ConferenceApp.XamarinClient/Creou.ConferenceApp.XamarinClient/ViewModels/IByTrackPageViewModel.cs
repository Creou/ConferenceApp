using System.Collections.Generic;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public interface IByTrackPageViewModel
	{
		List<ITrackViewModel> AvailableTracks { get; set; }

		int SelectedTrackIndex { get; set; }
		
		ITrackViewModel SelectedTrack { get; set; }

		IEnumerable<ISessionViewModel> Sessions { get; set; }

		void OnPageAppearing();
	}
}
