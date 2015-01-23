using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public partial class SessionsByTrackPage : BaseView
	{
		private readonly TrackViewModel _track;

		public SessionsByTrackPage(TrackViewModel track)
		{
			_track = track;

			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			((SessionsByTrackViewModel)this.BindingContext).Track = _track;
		}
	}
}
