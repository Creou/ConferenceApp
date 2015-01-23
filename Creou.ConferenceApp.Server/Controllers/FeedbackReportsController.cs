using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Creou.ConferenceApp.Server.Models;

namespace Creou.ConferenceApp.Server.Controllers
{
	[RoutePrefix("api/FeedbackReports")]
	public class FeedbackReportsController : ApiController
	{
		private CreouConferenceAppServerContext db = new CreouConferenceAppServerContext();

		// GET: api/FeedbackReports
		public IQueryable<FeedbackReport> GetFeedbackReports()
		{
			return db.FeedbackReports;
		}

		// GET: api/FeedbackReports/5
		[ResponseType(typeof(FeedbackReport))]
		public async Task<IHttpActionResult> GetFeedbackReport(int id)
		{
			FeedbackReport feedbackReport = await db.FeedbackReports.FindAsync(id);
			if (feedbackReport == null)
			{
				return NotFound();
			}

			return Ok(feedbackReport);
		}

		// GET: api/FeedbackReports/5
		[ResponseType(typeof(FeedbackReport)), Route("api/Sessions/{id}/FeedbackReports/{userId}")]
		public async Task<IHttpActionResult> GetFeedbackReport(int id, Guid userId)
		{
			FeedbackReport feedbackReport = await db.FeedbackReports.FirstOrDefaultAsync(r => r.Session.Id == id && r.Attendee.ClientId == userId);
			if (feedbackReport == null)
			{
				return NotFound();
			}

			return Ok(feedbackReport);
		}

		[ResponseType(typeof(IQueryable<FeedbackReport>)), Route("api/{userId}/FeedbackReports")]
		public async Task<IHttpActionResult> GetFeedbackReports(Guid userId)
		{
			var feedbackReports = await db.FeedbackReports
				.Where(r => r.Attendee.ClientId == userId)
				.Include(r => r.Attendee)
				.Include(r => r.Session)
				.ToListAsync();

			if (!feedbackReports.Any())
			{
				return NotFound();
			}

			return Ok(feedbackReports);
		}

		// POST: api/FeedbackReports
		[ResponseType(typeof(FeedbackReport))]
		public async Task<IHttpActionResult> PostFeedbackReport(FeedbackReportSubmission feedbackReport)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var report = await db.FeedbackReports.FirstOrDefaultAsync(fbr => fbr.Session.Id == feedbackReport.SessionId && fbr.Attendee.ClientId == feedbackReport.ClientId);
			
			var attendee = await db.Attendees.FirstOrDefaultAsync(a => a.ClientId == feedbackReport.ClientId) ?? new Attendee { ClientId = feedbackReport.ClientId };
			attendee.Name = string.IsNullOrWhiteSpace(feedbackReport.UserName) ? attendee.Name : feedbackReport.UserName;
			var session = await db.Sessions.FirstOrDefaultAsync(s => s.Id == feedbackReport.SessionId);

			if (report == null)
			{
				report = new FeedbackReport();
				db.FeedbackReports.Add(report);
			}

			report.Attendee = attendee;
			report.RatePresentation = feedbackReport.RatePresentation;
			report.RateContent = feedbackReport.RateContent;
			report.RateDelivery = feedbackReport.RateDelivery;
			report.RateSlides = feedbackReport.RateSlides;
			report.RateDemos = feedbackReport.RateDemos;
			report.LikeComments = feedbackReport.LikeComments;
			report.DislikeComments = feedbackReport.DislikeComments;
			report.GeneralComments = feedbackReport.GeneralComments;
			report.Session = session;

			await db.SaveChangesAsync();

			return CreatedAtRoute("DefaultApi", new { id = report.Id }, feedbackReport);
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