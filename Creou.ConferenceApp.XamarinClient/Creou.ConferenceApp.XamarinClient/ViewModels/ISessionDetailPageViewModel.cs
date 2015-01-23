using System.Windows.Input;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public interface ISessionDetailPageViewModel
	{
		ICommand PressRateSession { get; }

		ISessionViewModel Session { get; set; }
	}
}
