using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Creou.ConferenceApp.XamarinClient.ViewModels
{
	public interface IEventFeedbackPageViewModel
	{
		ICommand SubmitPressed { get; }

		ICommand CancelPressed { get; }

		bool FeedbackSavedLocally { get; }

		string UserName { get; set; }

		double RatingLocation { get; set; }

		string RatingLocationString { get; set; }

		double RatingVenue { get; set; }

		string RatingVenueString { get; set; }

		double RatingSessions { get; set; }

		string RatingSessionsString { get; set; }

		double RatingLunch { get; set; }

		string RatingLunchString { get; set; }

		//string LikeComments { get; set; }

		//string DislikeComments { get; set; }

		//string GeneralComments { get; set; }

		void PopulateForm();

		void PersistUserNameToStorage();
	}
}
