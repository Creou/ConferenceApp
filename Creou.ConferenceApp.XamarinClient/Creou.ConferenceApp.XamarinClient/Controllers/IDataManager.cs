using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Creou.ConferenceApp.XamarinClient.Models;
using Creou.ConferenceApp.XamarinClient.ViewModels;

namespace Creou.ConferenceApp.XamarinClient.Controllers
{
	public interface IDataManager
	{
		List<Session> Sessions { get; }

		List<FeedbackReport> FeedbackReports { get; }

		EventFeedbackReport EventFeedbackReport { get; set; }

		Enums.eDataLoadState SessionDataLoadState { get; }

		bool FeedbackDataLoaded { get; }

		Guid AppInstanceId { get; }

		DropDownOptionsGroup DropDownOptions { get; }

		Task InitialDataLoad();

		Task<IEnumerable<ITrackViewModel>> GetAvailableTracksAsync();

		Task<IEnumerable<ISessionViewModel>> GetSessionsByTrackAsync(ITrackViewModel track);
		
		Task<IEnumerable<DateTime>> GetAvailableSlotTimesAsync();
		
		Task<IEnumerable<Grouping<ISessionViewModel>>> GetSessionsByTimeAsync(DateTime time);
		
		void GenerateAndStoreAppInstanceIdIfNotExist();
		
		void SaveStringValueAsync(string value, string filename);
		
		Task<string> GetStoredStringValueAsync(string filename);

		Task<bool> PostFeedbackAsync(FeedbackReportSubmission feedback);

		Task<bool> PostEventFeedbackAsync(EventFeedbackReportSubmission eventFeedback);
	}
}
