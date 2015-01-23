using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.Views
{
	public partial class ByTrackTabbedPage : BaseTabbedView
	{
		public ByTrackTabbedPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
		}
	}
}
