using System.Windows.Input;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public interface ISessionFeedbackPageViewModel
	{
		ICommand SubmitPressed { get; }

		ICommand CancelPressed { get; }

		bool FeedbackSavedLocally { get; }

		ISessionViewModel Session { get; set; }

		string UserName { get; set; }
		
		double RatingPresentation { get; set; }

		string RatingPresentationString { get; set; }

		double RatingContent { get; set; }

		string RatingContentString { get; set; }
		
		double RatingDelivery { get; set; }

		string RatingDeliveryString { get; set; }

		double RatingSlides { get; set; }
	
		string RatingSlidesString { get; set; }

		double RatingDemos { get; set; }

		string RatingDemosString { get; set; }

		string LikeComments { get; set; }
		
		string DislikeComments { get; set; }

		string GeneralComments { get; set; }
		
		void PopulateForm();

		void PersistUserNameToStorage();
	}
}
