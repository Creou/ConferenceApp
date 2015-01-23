using System;

namespace Creou.ConferenceApp.Server.Models
{
	public class FeedbackReportSubmission
	{
		public Guid ClientId { get; set; }

		public string UserName { get; set; }

		public int SessionId { get; set; }

		public byte RatePresentation { get; set; }

		public byte RateContent { get; set; }

		public byte RateDelivery { get; set; }

		public byte RateSlides { get; set; }

		public byte RateDemos { get; set; }

		public string LikeComments { get; set; }

		public string  DislikeComments { get; set; }

		public string GeneralComments { get; set; }
	}
}