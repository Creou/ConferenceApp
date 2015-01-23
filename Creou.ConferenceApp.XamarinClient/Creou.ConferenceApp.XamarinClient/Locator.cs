using Creou.ConferenceApp.XamarinClient.Controllers;
using Creou.ConferenceApp.XamarinClient.Controllers.DefaultImplementations;
using Creou.ConferenceApp.XamarinClient.ViewModels;
using Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations;
using Microsoft.Practices.Unity;

namespace Creou.ConferenceApp.XamarinClient
{
	public class Locator
	{
		private readonly UnityContainer _container;

		public Locator()
		{
			_container = new UnityContainer();

			// Controllers
			_container.RegisterType<IDataManager, DataManager>(new ContainerControlledLifetimeManager());

			// ViewModels
			_container.RegisterType<IHomePageViewModel, HomePageViewModel>();
			_container.RegisterType<IByTimePageViewModel, ByTimePageViewModel>();
			_container.RegisterType<IByTrackPageViewModel, ByTrackPageViewModel>();
			_container.RegisterType<ISessionDetailPageViewModel, SessionDetailPageViewModel>();
			_container.RegisterType<ISessionFeedbackPageViewModel, SessionFeedbackPageViewModel>();
			_container.RegisterType<IEventFeedbackPageViewModel, EventFeedbackPageViewModel>();
			_container.RegisterType<ISessionViewModel, SessionViewModel>();
			_container.RegisterType<ITrackViewModel, TrackViewModel>();
		}

		public IDataManager DataManager
		{
			get { return _container.Resolve<IDataManager>(); }
		}

		public IHomePageViewModel HomePageViewModel
		{
			get { return _container.Resolve<IHomePageViewModel>(); }
		}

		public IByTimePageViewModel ByTimePageViewModel
		{
			get { return _container.Resolve<IByTimePageViewModel>(); }
		}

		public IByTrackPageViewModel ByTrackPageViewModel
		{
			get { return _container.Resolve<IByTrackPageViewModel>(); }
		}

		public ISessionDetailPageViewModel SessionDetailPageViewModel
		{
			get { return _container.Resolve<ISessionDetailPageViewModel>(); }
		}

		public ISessionFeedbackPageViewModel SessionFeedbackPageViewModel
		{
			get { return _container.Resolve<ISessionFeedbackPageViewModel>(); }
		}

		public IEventFeedbackPageViewModel EventFeedbackPageViewModel
		{
			get { return _container.Resolve<IEventFeedbackPageViewModel>(); }
		}

		public ISessionViewModel SessionViewModel
		{
			get { return _container.Resolve<ISessionViewModel>(); }
		}

		public ITrackViewModel TrackViewModel
		{
			get { return _container.Resolve<ITrackViewModel>(); }
		}
	}
}
