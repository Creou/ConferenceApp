using Creou.ConferenceApp.XamarinClient.WinPhone.Resources;

namespace Creou.ConferenceApp.XamarinClient.WinPhone
{
	/// <summary>
	/// Provides access to string resources.
	/// </summary>
	public class LocalizedStrings
	{
		private static AppResources _localizedResources = new AppResources();

		public AppResources LocalizedResources { get { return _localizedResources; } }
	}
}
