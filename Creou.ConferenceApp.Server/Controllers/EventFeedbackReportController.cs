using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Creou.ConferenceApp.Server.Models;

namespace Creou.ConferenceApp.Server.Controllers
{
	public class EventFeedbackReportController : ApiController
	{
		private CreouConferenceAppServerContext db = new CreouConferenceAppServerContext();

		// GET: api/EventFeedbackReports
		public IQueryable<EventFeedbackReport> GetEventFeedbackReports()
		{
			return db.EventFeedbackReports;
		}

		// GET: api/EventFeedbackReports/5
		[ResponseType(typeof(EventFeedbackReport))]
		public async Task<IHttpActionResult> GetEventFeedbackReport(int id)
		{
			EventFeedbackReport eventFeedbackReport = await db.EventFeedbackReports.FindAsync(id);
			if (eventFeedbackReport == null)
			{
				return NotFound();
			}

			return Ok(eventFeedbackReport);
		}

		// GET: api/EventFeedbackReports/5
		[ResponseType(typeof(EventFeedbackReport)), Route("api/EventFeedbackReports/{userId}")]
		public async Task<IHttpActionResult> GetEventFeedbackReport(Guid userId)
		{
			var eventFeedbackReport = await db.EventFeedbackReports.Include(i => i.Attendee).FirstOrDefaultAsync(r => r.Attendee.ClientId == userId);
			if (eventFeedbackReport == null)
			{
				return NotFound();
			}

			return Ok(eventFeedbackReport);
		}

		// POST: api/EventFeedbackReports
		[ResponseType(typeof(EventFeedbackReport)), Route("api/EventFeedbackReports")]
		public async Task<IHttpActionResult> PostEventFeedbackReport(EventFeedbackReportSubmission feedbackReport)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var report = await db.EventFeedbackReports.FirstOrDefaultAsync(fbr => fbr.Attendee.ClientId == feedbackReport.ClientId);

			var attendee = await db.Attendees.FirstOrDefaultAsync(a => a.ClientId == feedbackReport.ClientId) ?? new Attendee { ClientId = feedbackReport.ClientId };
			attendee.Name = string.IsNullOrWhiteSpace(feedbackReport.UserName) ? attendee.Name : feedbackReport.UserName;

			if (report == null)
			{
				report = new EventFeedbackReport();
				db.EventFeedbackReports.Add(report);
			}

			report.Attendee = attendee;
			report.RateLocation = feedbackReport.RateLocation;
			report.RateVenue = feedbackReport.RateVenue;
			report.RateSessions = feedbackReport.RateSessions;
			report.RateLunch = feedbackReport.RateLunch;
			report.LikeComments = feedbackReport.LikeComments;
			report.DislikeComments = feedbackReport.DislikeComments;
			report.SuggestedLocation = feedbackReport.SuggestedLocation;
			report.YearsInvolved = feedbackReport.YearsInvolved;
			report.TechnologiesUsed = feedbackReport.TechnologiesUsed;
			report.DiscoveryMethod = feedbackReport.DiscoveryMethod;
			report.AttendeeLocation = feedbackReport.AttendeeLocation;
			report.TransportationMethod = feedbackReport.TransportationMethod;
			report.CompanySize = feedbackReport.CompanySize;
			report.Status = feedbackReport.Status;

			await db.SaveChangesAsync();

			return Created("api/EventFeedbackReports/" + report.Attendee.ClientId, report);
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}