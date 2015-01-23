namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class TrackViewModel : BaseViewModel, ITrackViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}