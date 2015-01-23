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
	public class ByTrackDropDownOptionValuesController : ApiController
	{
		private CreouConferenceAppServerContext db = new CreouConferenceAppServerContext();

		// GET: api/DropDownOptionValues/CompanySize
		[ResponseType(typeof(IQueryable<DropDownOptionValue>)), Route("api/DropDownOptionValues/{type}")]
		public IHttpActionResult GetDropDownOptionValues(string type)
		{
			var dropDownOptionValues = db.DropDownOptionValues.Where(v => v.Option.OptionName == type).ToList();
			if (!dropDownOptionValues.Any())
			{
				return NotFound();
			}

			return Ok(dropDownOptionValues);
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