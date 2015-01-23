using System.Windows.Input;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public interface IHomePageViewModel
	{
		ICommand PressByTime { get; }

		ICommand PressByTrack { get; }

		ICommand PressEventFeedback { get; }

		bool DataLoaded { get; set; }
	}
}
