using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var local = PropertyChanged;
			if (local != null)
			{
				local(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged();
			}
		}

		private string _heading;

		public string Heading
		{
			get { return _heading; }
			set
			{
				_heading = value;
				OnPropertyChanged();
			}
		}

		public double FontSmall
		{
			get
			{
				return Device.OnPlatform(iOS: 14, Android: 14, WinPhone: 18);
			}
		}

		public double FontMedium
		{
			get
			{
				return Device.OnPlatform(iOS: 16, Android: 16, WinPhone: 22);
			}
		}

		public double FontLarge
		{
			get
			{
				return Device.OnPlatform(iOS: 18, Android: 18, WinPhone: 24);
			}
		}

		public double FontExtraLarge
		{
			get
			{
				return Device.OnPlatform(iOS: 22, Android: 22, WinPhone: 28);
			}
		}

		protected void ShowErrorMessage(string messageText, string caption = "Error")
		{
			MessagingCenter.Send(new NavigationMessage { Parameter = messageText, Parameter2 = caption },
				Enums.eNavigationMessage.ShowErrorMessage.ToString());
		}

		protected void ClosePage()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.ClosePage.ToString());
		}
	}
}
