using System;
using System.Collections.Generic;
using System.Windows.Input;
using Creou.ConferenceApp.XamarinClient.Models;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public interface IByTimePageViewModel
	{
		ICommand PressLeft { get; }

		ICommand PressRight { get; }

		List<DateTime> AvailableSlotTimes { get; set; }
		
		int SelectedTimeIndex { get; set; }
		
		string SelectedTimeString { get; }
		
		IEnumerable<Grouping<ISessionViewModel>> GroupedSessions { get; set; }

		void OnPageAppearing();
	}
}
