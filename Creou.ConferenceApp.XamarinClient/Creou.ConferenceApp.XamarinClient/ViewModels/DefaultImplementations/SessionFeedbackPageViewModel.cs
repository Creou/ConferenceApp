using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Creou.ConferenceApp.XamarinClient;
using Creou.ConferenceApp.XamarinClient.Controllers;
using Creou.ConferenceApp.XamarinClient.Controllers.DefaultImplementations;
using Creou.ConferenceApp.XamarinClient.Models;
using DeviceInfo.Plugin;
using PCLCrypto;
using Xamarin.Forms;

namespace Creou.ConferenceApp.XamarinClient.ViewModels.DefaultImplementations
{
	public class SessionFeedbackPageViewModel : BaseViewModel, ISessionFeedbackPageViewModel
	{
		private IDataManager _dataManager;

		public ICommand SubmitPressed { get; private set; }

		public ICommand CancelPressed { get; private set; }

		public bool FeedbackSavedLocally { get; private set; }

		private ISessionViewModel _session;

		public ISessionViewModel Session
		{
			get { return _session; }
			set
			{
				_session = value;
				OnPropertyChanged();
			}
		}

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

		private double _ratingPresentation;

		public double RatingPresentation
		{
			get { return _ratingPresentation; }
			set
			{
				_ratingPresentation = Math.Round(value, 0);
				if (_ratingPresentation == 0) { _ratingPresentation = 1; }
				RatingPresentationString = _ratingPresentation.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingPresentationString;

		public string RatingPresentationString
		{
			get { return _ratingPresentationString; }
			set
			{
				_ratingPresentationString = value;
				OnPropertyChanged();
			}
		}

		// *********************************************

		private double _ratingContent;

		public double RatingContent
		{
			get { return _ratingContent; }
			set
			{
				_ratingContent = Math.Round(value, 0);
				if (_ratingContent == 0) { _ratingContent = 1; }
				RatingContentString = _ratingContent.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingContentString;

		public string RatingContentString
		{
			get { return _ratingContentString; }
			set
			{
				_ratingContentString = value;
				OnPropertyChanged();
			}
		}

		// *********************************************

		private double _ratingDelivery;

		public double RatingDelivery
		{
			get { return _ratingDelivery; }
			set
			{
				_ratingDelivery = Math.Round(value, 0);
				if (_ratingDelivery == 0) { _ratingDelivery = 1; }
				RatingDeliveryString = _ratingDelivery.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingDeliveryString;

		public string RatingDeliveryString
		{
			get { return _ratingDeliveryString; }
			set
			{
				_ratingDeliveryString = value;
				OnPropertyChanged();
			}
		}

		// *********************************************

		private double _ratingSlides;

		public double RatingSlides
		{
			get { return _ratingSlides; }
			set
			{
				_ratingSlides = Math.Round(value, 0);
				if (_ratingSlides == 0) { _ratingSlides = 1; }
				RatingSlidesString = _ratingSlides.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingSlidesString;

		public string RatingSlidesString
		{
			get { return _ratingSlidesString; }
			set
			{
				_ratingSlidesString = value;
				OnPropertyChanged();
			}
		}

		// *********************************************

		private double _ratingDemos;

		public double RatingDemos
		{
			get { return _ratingDemos; }
			set
			{
				_ratingDemos = Math.Round(value, 0);
				if (_ratingDemos == 0) { _ratingDemos = 1; }
				RatingDemosString = _ratingDemos.ToString();
				OnPropertyChanged();
			}
		}

		private string _ratingDemosString;

		public string RatingDemosString
		{
			get { return _ratingDemosString; }
			set
			{
				_ratingDemosString = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region CommentsTextProperties

		private string _likeComments;

		public string LikeComments
		{
			get { return _likeComments; }
			set
			{
				_likeComments = value;
				OnPropertyChanged();
			}
		}

		private string _dislikeComments;

		public string DislikeComments
		{
			get { return _dislikeComments; }
			set
			{
				_dislikeComments = value;
				OnPropertyChanged();
			}
		}

		private string _generalComments;

		public string GeneralComments
		{
			get { return _generalComments; }
			set
			{
				_generalComments = value;
				OnPropertyChanged();
			}
		}

		#endregion

		public SessionFeedbackPageViewModel(IDataManager dataManager)
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
			var existingFeedback = GetExisingSessionFeedback();

			if (existingFeedback != null)
			{
				RatingPresentation = existingFeedback.RatePresentation;
				RatingContent = existingFeedback.RateContent;
				RatingDelivery = existingFeedback.RateDelivery;
				RatingSlides = existingFeedback.RateSlides;
				RatingDemos = existingFeedback.RateDemos;

				LikeComments = existingFeedback.LikeComments;
				DislikeComments = existingFeedback.DislikeComments;
				GeneralComments = existingFeedback.GeneralComments;
			}
			else
			{
				RatingPresentationString = "-";
				RatingContentString = "-";
				RatingDeliveryString = "-";
				RatingSlidesString = "-";
				RatingDemosString = "-";
			}
		}

		private FeedbackReport GetExisingSessionFeedback()
		{
			if (_dataManager.FeedbackReports != null)
			{
				return _dataManager.FeedbackReports.FirstOrDefault(fr => fr.Session.Id == Session.Session.Id);
			}
			else
			{
				return null;				
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
				ShowErrorMessage("Please complete all ratings before submitting.", "Session Feedback");
			}
			else
			{
				var feedback = CreateFeedbackReportSubmission();

				_dataManager.FeedbackReports.Add(ConvertReportSubmissionToFeedbackReport(feedback));
				FeedbackSavedLocally = true;

				var feedbackPostSuccess = await _dataManager.PostFeedbackAsync(feedback);

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

		private FeedbackReportSubmission CreateFeedbackReportSubmission()
		{
			return new FeedbackReportSubmission
			{
				ClientId = _dataManager.AppInstanceId,
				UserName = UserName,
				SessionId = Session.Session.Id,

				RatePresentation = Convert.ToByte(RatingPresentation),
				RateContent = Convert.ToByte(RatingContent),
				RateDelivery = Convert.ToByte(RatingDelivery),
				RateSlides = Convert.ToByte(RatingSlides),
				RateDemos = Convert.ToByte(RatingDemos),

				LikeComments = LikeComments,
				DislikeComments = DislikeComments,
				GeneralComments = GeneralComments
			};
		}

		private FeedbackReport ConvertReportSubmissionToFeedbackReport(FeedbackReportSubmission reportSubmission)
		{
			var feedback = new FeedbackReport
			{
				Session = Session.Session,

				RatePresentation = reportSubmission.RatePresentation,
				RateContent = reportSubmission.RateContent,
				RateDelivery = reportSubmission.RateDelivery,
				RateSlides = reportSubmission.RateSlides,
				RateDemos = reportSubmission.RateDemos,

				LikeComments = reportSubmission.LikeComments,
				DislikeComments = reportSubmission.DislikeComments,
				GeneralComments = reportSubmission.GeneralComments
			};

			return feedback;
		}

		//private Guid DeviceId()
		//{
		//	var deviceInfo = CrossDeviceInfo.Current;

		//	byte[] salt = { 1, 2, 3, 4, 5, 6, 7, 8 }; // best initialized to a unique value for each user, and stored with the user record
		//	int iterations = 10; // higher makes brute force attacks more expensive
		//	int keyLengthInBytes = 16;
		//	byte[] key = NetFxCrypto.DeriveBytes.GetBytes(deviceInfo.Id, salt, iterations, keyLengthInBytes);
			
		//	return new Guid(key);
		//}

		private void CancelFeedback()
		{
			MessagingCenter.Send(new NavigationMessage(),
				Enums.eNavigationMessage.CancelFeedbackConfirmation.ToString());
		}

		private bool AllRatingsEntered()
		{
			return (
				RatingPresentation > 0 &&
				RatingContent > 0 &&
				RatingDelivery > 0 &&
				RatingSlides > 0 &&
				RatingDemos > 0);
		}
	}
}
