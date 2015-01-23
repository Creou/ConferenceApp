using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creou.ConferenceApp.XamarinClient
{
	public class Enums
	{
		public enum eNavigationMessage
		{
			ShowByTimePage,
			ShowByTrackPage,
			ShowErrorMessage,
			ShowTimeButtons,
			HideTimeButtons,
			ShowPicker,
			HidePicker,
			ShowSessionDetailPage,
			ShowSessionFeedbackPage,
			ShowEventFeedbackPage,
			HideNavBar,
			CancelFeedbackConfirmation,
			ClosePage
		}

		public enum eDataLoadState
		{
			NoDataLoaded,
			DataLoadedFromLocalCache,
			DataLoadedFromServer
		}
	}
}
