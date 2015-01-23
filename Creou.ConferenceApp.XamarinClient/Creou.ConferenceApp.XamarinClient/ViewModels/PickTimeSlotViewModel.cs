using System;
using System.Collections.Generic;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public class PickTimeSlotViewModel : BaseViewModel
	{
		public IEnumerable<SlotTimeViewModel> Times { get; set; }
	}
}