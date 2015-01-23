using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public class PickTrackViewModel : BaseViewModel
	{
		private List<TrackViewModel> _trackList;

		public List<TrackViewModel> TrackList
		{
			get { return _trackList; }
			set
			{
				_trackList = value;
				OnPropertyChanged();
			}
		}

		private DataManager _dataManager;

		public PickTrackViewModel()
		{
			_dataManager = App.Data;

			Task.Run(() =>
			{
				try
				{
					IsBusy = true;
					TrackList = _dataManager.GetAvailableTracksAsync().Result.ToList();
					Heading = "Please pick a track:";
					IsBusy = false;
				}
				catch (Exception ex)
				{
					IsBusy = false;
					ShowErrorMessage("No internets");
				}
			});
		}

		private void ShowErrorMessage(string messageText)
		{
			MessagingCenter.Send(new NavigationMessage { Parameter = messageText },
				Enums.eNavigationMessage.ShowErrorMessage.ToString());
		}
	}
}