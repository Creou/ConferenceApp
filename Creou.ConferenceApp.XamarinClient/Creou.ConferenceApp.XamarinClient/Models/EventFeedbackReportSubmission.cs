using System;

namespace Creou.ConferenceApp.XamarinClient.Models
{
	public class EventFeedbackReportSubmission
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public Guid ClientId { get; set; }

		public byte RateLocation { get; set; }

		public byte RateVenue { get; set; }

		public byte RateSessions { get; set; }

		public byte RateLunch { get; set; }

		public string LikeComments { get; set; }

		public string DislikeComments { get; set; }

		public string SuggestedLocation { get; set; }

		public int CompanySize { get; set; }

		public byte YearsInvolved { get; set; }

		public int Status { get; set; }

		public string TechnologiesUsed { get; set; }

		public string DiscoveryMethod { get; set; }

		public string AttendeeLocation { get; set; }

		public string TransportationMethod { get; set; }
	}
}