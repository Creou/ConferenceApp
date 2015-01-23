using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Creou.ConferenceApp.Server.Models
{
	public class FeedbackReport
	{
		public int Id { get; set; }

		public Attendee Attendee { get; set; }

		public Session Session { get; set; }

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