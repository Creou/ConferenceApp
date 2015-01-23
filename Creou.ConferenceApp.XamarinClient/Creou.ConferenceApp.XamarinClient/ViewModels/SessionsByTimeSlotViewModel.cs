using System;
using System.Collections.Generic;
using Creou.ConferenceApp.XamarinClient.Models;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public class SessionsByTimeSlotViewModel : BaseViewModel
	{
		public SlotTimeViewModel SlotTime { get; set; }

		public string Heading { get; set; }

		public IEnumerable<SessionViewModel> Sessions { get; set; }
	}
}