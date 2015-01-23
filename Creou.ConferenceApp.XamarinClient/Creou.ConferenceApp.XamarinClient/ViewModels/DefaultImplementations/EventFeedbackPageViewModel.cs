using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Creou.ConferenceApp.XamarinClient.Controllers;
using Creou.ConferenceApp.XamarinClient.Models;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class EventFeedbackPageViewModel : BaseViewModel, IEventFeedbackPageViewModel
	{
		private IDataManager _dataManager;

		public ICommand SubmitPressed { get; private set; }

		public ICommand CancelPressed { get; private set; }

		public bool FeedbackSavedLocally { get; private set; }

		private string _userName;

		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				OnPropertyChanged();
			}
		}

		#region RatingSliderProperties

		// *********************************************
		
		private double _ratingLocation;

		public double RatingLocation
		{
			get { return _ratingLocation; }
			set
			{
				_ratingLocation = Math.Round(value, 0);
				if (_ratingLocation == 0) { _ratingLocation = 1; }
				RatingLocationString = _ratingLocation.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingLocationString;

		public string RatingLocationString
		{
			get { return _ratingLocationString; }
			set
			{
				_ratingLocationString = value;
				OnPropertyChanged();
			}
		}

		// *********************************************

		private double _ratingVenue;

		public double RatingVenue
		{
			get { return _ratingVenue; }
			set
			{
				_ratingVenue = Math.Round(value, 0);
				if (_ratingVenue == 0) { _ratingVenue = 1; }
				RatingVenueString = _ratingVenue.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingVenueString;

		public string RatingVenueString
		{
			get { return _ratingVenueString; }
			set
			{
				_ratingVenueString = value;
				OnPropertyChanged();
			}
		}

		// *********************************************

		private double _ratingSessions;

		public double RatingSessions
		{
			get { return _ratingSessions; }
			set
			{
				_ratingSessions = Math.Round(value, 0);
				if (_ratingSessions == 0) { _ratingSessions = 1; }
				RatingSessionsString = _ratingSessions.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingSessionsString;

		public string RatingSessionsString
		{
			get { return _ratingSessionsString; }
			set
			{
				_ratingSessionsString = value;
				OnPropertyChanged();
			}
		}

		// *********************************************

		private double _ratingLunch;

		public double RatingLunch
		{
			get { return _ratingLunch; }
			set
			{
				_ratingLunch = Math.Round(value, 0);
				if (_ratingLunch == 0) { _ratingLunch = 1; }
				RatingLunchString = _ratingLunch.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingLunchString;

		public string RatingLunchString
		{
			get { return _ratingLunchString; }
			set
			{
				_ratingLunchString = value;
				OnPropertyChanged();
			}
		}
		#endregion

		public EventFeedbackPageViewModel(IDataManager dataManager)
		{
			_dataManager = dataManager;

			SubmitPressed = new Command(AttemptSubmissionAsync);
			CancelPressed = new Command(CancelFeedback);

			Task.Run(
				async () =>
					UserName = await _dataManager.GetStoredStringValueAsync(Constants.UserNameFilename));
		}

		public void PopulateForm()
		{
			if (_dataManager.EventFeedbackReport != null)
			{
				RatingLocation = _dataManager.EventFeedbackReport.RateLocation;
				RatingVenue = _dataManager.EventFeedbackReport.RateVenue;
				RatingSessions = _dataManager.EventFeedbackReport.RateSessions;
				RatingLunch = _dataManager.EventFeedbackReport.RateLunch;

				//LikeComments = _dataManager.EventFeedbackReport.LikeComments;
				//DislikeComments = _dataManager.EventFeedbackReport.DislikeComments;
				//GeneralComments = _dataManager.EventFeedbackReport.GeneralComments;
			}
			else
			{
				RatingLocationString = "-";
				RatingVenueString = "-";
				RatingSessionsString = "-";
				RatingLunchString = "-";
			}
		}

		public void PersistUserNameToStorage()
		{
			_dataManager.SaveStringValueAsync(UserName, Constants.UserNameFilename);
		}

		private async void AttemptSubmissionAsync()
		{
			if (!AllRatingsEntered())
			{
				ShowErrorMessage("Please complete all ratings before submitting.", "Event Feedback");
			}
			else
			{
				var eventFeedback = CreateEventFeedbackReportSubmission();

				_dataManager.EventFeedbackReport = ConvertReportSubmissionToFeedbackReport(eventFeedback);
				FeedbackSavedLocally = true;

				var feedbackPostSuccess = await _dataManager.PostEventFeedbackAsync(eventFeedback);

				if (!feedbackPostSuccess)
				{
					ShowErrorMessage("Failed to post feedback to the server. Please try again later.");
				}
				else
				{
					ShowErrorMessage("Thanks for your contribution.", "Session Feedback");
					ClosePage();
				}
			}
		}

		private EventFeedbackReportSubmission CreateEventFeedbackReportSubmission()
		{
			return new EventFeedbackReportSubmission
			{
				ClientId = _dataManager.AppInstanceId,
				UserName = UserName,

				RateLocation = Convert.ToByte(RatingLocation),
				RateVenue = Convert.ToByte(RatingVenue),
				RateSessions = Convert.ToByte(RatingSessions),
				RateLunch = Convert.ToByte(RatingLunch),

				//LikeComments = LikeComments,
				//DislikeComments = DislikeComments,
				//GeneralComments = GeneralComments
			};
		}

		private EventFeedbackReport ConvertReportSubmissionToFeedbackReport(EventFeedbackReportSubmission reportSubmission)
		{
			var feedback = new EventFeedbackReport
			{
				RateLocation = reportSubmission.RateLocation,
				RateVenue = reportSubmission.RateVenue,
				RateSessions = reportSubmission.RateSessions,
				RateLunch = reportSubmission.RateLunch,
				
				//LikeComments = reportSubmission.LikeComments,
				//DislikeComments = reportSubmission.DislikeComments,
				//GeneralComments = reportSubmission.GeneralComments
			};

			return feedback;
		}

		private void CancelFeedback()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.CancelFeedbackConfirmation.ToString());
		}

		private bool AllRatingsEntered()
		{
			return (
				RatingLocation > 0 &&
				RatingVenue > 0 &&
				RatingSessions > 0 &&
				RatingLunch > 0);
		}
	}
}
