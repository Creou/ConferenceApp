using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Creou.ConferenceApp.Server.Models;

namespace Creou.ConferenceApp.Server.Controllers
{
	public class SessionFeedbackController : Controller
	{
		private readonly CreouConferenceAppServerContext _db = new CreouConferenceAppServerContext();

		public async Task<ActionResult> ForSession(int Id)
		{
			var feedbackReport =
				await _db.FeedbackReports.Include(r => r.Session).FirstOrDefaultAsync(r => r.Session.Id == Id)
				?? new FeedbackReport { Session = new Session()};
			return View("Index", feedbackReport);
		}

		[HttpPost]
		public async Task<ActionResult> Post(FeedbackReport feedback)
		{
			if (ModelState.IsValid)
			{
				_db.Entry(feedback).State = EntityState.Modified;
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View("Index", feedback);
		}

		/*
		// GET: SessionFeedback
		public async Task<ActionResult> Index()
		{
			return View(await db.FeedbackReports.ToListAsync());
		}

		// GET: SessionFeedback/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			FeedbackReport feedbackReport = await db.FeedbackReports.FindAsync(id);
			if (feedbackReport == null)
			{
				return HttpNotFound();
			}
			return View(feedbackReport);
		}

		// GET: SessionFeedback/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: SessionFeedback/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,RatePresentation,RateContent,RateDelivery,RateSlides,RateDemos,LikeComments,DislikeComments,GeneralComments")] FeedbackReport feedbackReport)
		{
			if (ModelState.IsValid)
			{
				db.FeedbackReports.Add(feedbackReport);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(feedbackReport);
		}

		// GET: SessionFeedback/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			FeedbackReport feedbackReport = await db.FeedbackReports.FindAsync(id);
			if (feedbackReport == null)
			{
				return HttpNotFound();
			}
			return View(feedbackReport);
		}

		// POST: SessionFeedback/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,RatePresentation,RateContent,RateDelivery,RateSlides,RateDemos,LikeComments,DislikeComments,GeneralComments")] FeedbackReport feedbackReport)
		{
			if (ModelState.IsValid)
			{
				db.Entry(feedbackReport).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(feedbackReport);
		}

		// GET: SessionFeedback/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			FeedbackReport feedbackReport = await db.FeedbackReports.FindAsync(id);
			if (feedbackReport == null)
			{
				return HttpNotFound();
			}
			return View(feedbackReport);
		}

		// POST: SessionFeedback/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			FeedbackReport feedbackReport = await db.FeedbackReports.FindAsync(id);
			db.FeedbackReports.Remove(feedbackReport);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}*/
	}
}
