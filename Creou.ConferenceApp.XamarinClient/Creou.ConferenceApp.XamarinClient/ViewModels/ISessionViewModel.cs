using System.Windows.Input;
using Creou.ConferenceApp.XamarinClient.Models;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public interface ISessionViewModel
	{
		Session Session { get; set; }

		string SessionTitleUpper { get; set; }

		string SessionTitleShort { get; set; }

		string TimesString { get; set; }

		string TimesRoomAndTrackString { get; set; }

		ICommand SessionSelected { get; }

		bool SessionFinished { get; }
	}
}
