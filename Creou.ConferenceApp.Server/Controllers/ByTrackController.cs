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
using Creou.ConferenceApp.Server.ViewModels;

namespace Creou.ConferenceApp.Server.Controllers
{
	public class ByTrackController : Controller
	{
		private readonly CreouConferenceAppServerContext _db = new CreouConferenceAppServerContext();
		
		// GET: ByTrack
		//public async Task<ActionResult> Index()
		//{
		//	return await Index(1);
		//}

		// GET: ByTrack/1
		public async Task<ActionResult> Index(int? id)
		{
			id = id ?? 1;
			var sessions = await _db.Sessions.Where(s => s.Track.Id == id).OrderBy(s => s.Start).Include(d=>d.Room).Include(d=>d.Speaker).AsNoTracking().ToListAsync() ?? new List<Session>();
			var tracks = await _db.Tracks.OrderBy(s => s.Name).AsNoTracking().ToListAsync();
			var byTracks = new ByTrack(tracks, sessions, id.Value);
			return View(byTracks);
		}

		[HttpPost]
		public ActionResult Index(ByTrack model)
		{
			if (ModelState.IsValid)
			{
				var id = model.SelectedTrackId;
				return RedirectToAction(string.Format("Index/{0}",id));
			}
			return View("Index", model);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}



//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Creou.ConferenceApp.Server.Models;

//namespace Creou.ConferenceApp.Server.Controllers
//{
//	public class ByTrack : Controller
//	{
//		private CreouConferenceAppServerContext db = new CreouConferenceAppServerContext();

//		// GET: ByTrack
//		public async Task<ActionResult> Index()
//		{
//			return View(await db.Sessions.ToListAsync());
//		}

//		// GET: ByTrack/Details/5
//		public async Task<ActionResult> Details(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//			Session session = await db.Sessions.FindAsync(id);
//			if (session == null)
//			{
//				return HttpNotFound();
//			}
//			return View(session);
//		}

//		// GET: ByTrack/Create
//		public ActionResult Create()
//		{
//			return View();
//		}

//		// POST: ByTrack/Create
//		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//		[HttpPost]
//		[ValidateAntiForgeryToken]
//		public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Start,End")] Session session)
//		{
//			if (ModelState.IsValid)
//			{
//				db.Sessions.Add(session);
//				await db.SaveChangesAsync();
//				return RedirectToAction("Index");
//			}

//			return View(session);
//		}

//		// GET: ByTrack/Edit/5
//		public async Task<ActionResult> Edit(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//			Session session = await db.Sessions.FindAsync(id);
//			if (session == null)
//			{
//				return HttpNotFound();
//			}
//			return View(session);
//		}

//		// POST: ByTrack/Edit/5
//		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//		[HttpPost]
//		[ValidateAntiForgeryToken]
//		public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Start,End")] Session session)
//		{
//			if (ModelState.IsValid)
//			{
//				db.Entry(session).State = EntityState.Modified;
//				await db.SaveChangesAsync();
//				return RedirectToAction("Index");
//			}
//			return View(session);
//		}

//		// GET: ByTrack/Delete/5
//		public async Task<ActionResult> Delete(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//			Session session = await db.Sessions.FindAsync(id);
//			if (session == null)
//			{
//				return HttpNotFound();
//			}
//			return View(session);
//		}

//		// POST: ByTrack/Delete/5
//		[HttpPost, ActionName("Delete")]
//		[ValidateAntiForgeryToken]
//		public async Task<ActionResult> DeleteConfirmed(int id)
//		{
//			Session session = await db.Sessions.FindAsync(id);
//			db.Sessions.Remove(session);
//			await db.SaveChangesAsync();
//			return RedirectToAction("Index");
//		}

//		protected override void Dispose(bool disposing)
//		{
//			if (disposing)
//			{
//				db.Dispose();
//			}
//			base.Dispose(disposing);
//		}
//	}
//}
