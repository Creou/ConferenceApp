using System;

namespace Creou.ConferenceApp.XamarinClient.Models
{
	public class Attendee
	{
		public int Id { get; set; }

		public Guid ClientId { get; set; }

		public string Name { get; set; }
	}
}