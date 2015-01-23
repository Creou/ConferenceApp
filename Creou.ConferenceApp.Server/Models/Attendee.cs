using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Creou.ConferenceApp.Server.Models
{
	public class Attendee
	{
		public int Id { get; set; }

		public Guid ClientId { get; set; }

		public string Name { get; set; }
	}
}