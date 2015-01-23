using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Creou.ConferenceApp.XamarinClient.Controllers;
using Creou.ConferenceApp.XamarinClient.Controllers.DefaultImplementations;
using Creou.ConferenceApp.XamarinClient.Models;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class ByTimePageViewModel : BaseViewModel, IByTimePageViewModel
	{
		private IDataManager _dataManager;

		public ICommand PressLeft { get; private set; }

		public ICommand PressRight { get; private set; }

		private List<DateTime> _availableSlotTimes;

		public List<DateTime> AvailableSlotTimes
		{
			get { return _availableSlotTimes; }
			set
			{
				_availableSlotTimes = value;
				//SelectedTimeIndex = 0;
				SelectedTimeIndex = App.ByTimePageSelectedIndex;
			}
		}

		private int _selectedTimeIndex;

		public int SelectedTimeIndex
		{
			get { return _selectedTimeIndex; }
			set
			{
				_selectedTimeIndex = value;
				OnPropertyChanged("SelectedTimeString");
				
				Task.Run(async () =>
					GroupedSessions = await _dataManager.GetSessionsByTimeAsync(AvailableSlotTimes[SelectedTimeIndex]));
			}
		}

		public string SelectedTimeString
		{
			get
			{
				if (AvailableSlotTimes == null)
				{
					return string.Empty;
				}
				return timeStringDisplayFormat(AvailableSlotTimes[SelectedTimeIndex]);
			}
		}

		private IEnumerable<Grouping<ISessionViewModel>> _groupedSessions;

		public IEnumerable<Grouping<ISessionViewModel>> GroupedSessions
		{
			get { return _groupedSessions; }
			set
			{
				_groupedSessions = value;
				OnPropertyChanged();
				ShowTimeButtons();
			}
		}

		public ByTimePageViewModel(IDataManager dataManager)
		{
			_dataManager = dataManager;

			PressLeft = new Command(GoToEarlierTime);
			PressRight = new Command(GoToLaterTime);

			LoadData();
		}

		private void LoadData()
		{
			Task.Run(() =>
			{
				try
				{
					IsBusy = true;
					AvailableSlotTimes = _dataManager.GetAvailableSlotTimesAsync().Result.ToList();
					IsBusy = false;
				}
				catch (HttpRequestException ex)
				{
					IsBusy = false;
					ShowErrorMessage("No internets");
				}
			});
		}

		public void OnPageAppearing()
		{
			if (AvailableSlotTimes != null)
			{
				ShowTimeButtons();
			}
		}

		private void GoToEarlierTime()
		{
			if (SelectedTimeIndex > 0)
			{
				SelectedTimeIndex = SelectedTimeIndex - 1;
			}
		}

		private void GoToLaterTime()
		{
			if (SelectedTimeIndex < AvailableSlotTimes.Count - 1)
			{
				SelectedTimeIndex = SelectedTimeIndex + 1;
			}
		}

		private string timeStringDisplayFormat(DateTime input)
		{
			return input.ToString("h:mm tt");
		}

		private void ShowTimeButtons()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.ShowTimeButtons.ToString());
		}

		private void HideTimeButtons()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.HideTimeButtons.ToString());
		}
	}
}
