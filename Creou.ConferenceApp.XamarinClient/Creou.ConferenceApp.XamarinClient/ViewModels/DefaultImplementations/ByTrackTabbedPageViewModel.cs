using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.Controllers.DefaultImplementations;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class ByTrackTabbedPageViewModel : BaseViewModel
	{
		public List<ByTrackTabbedPageViewModelChild> Children { get; set; }

		private DataManager _dataManager;

		public List<TrackViewModel> AvailableTracks { get; set; }

		public ByTrackTabbedPageViewModel()
		{
			_dataManager = App.DataManager;

			LoadData();
		}

		private void LoadData()
		{
			Task.Run(() =>
			{
				IsBusy = true;
				AvailableTracks = _dataManager.GetAvailableTracksAsync().Result.ToList();

				Children = new List<ByTrackTabbedPageViewModelChild>();

				foreach (var track in AvailableTracks)
				{
					Children.Add(new ByTrackTabbedPageViewModelChild
					{
						Track = track,
						Sessions = _dataManager.GetSessionsByTrackAsync(track).Result
					});
				}

				IsBusy = false;
			});
		}
	}
}
