using System;

namespace Creou.ConferenceApp.XamarinClient.Models
{
	public class Session
	{
		public Session() { }

		public Session(Tuple<TimeSpan, TimeSpan> timing)
		{
			Start = new DateTime(2014, 1, 1).Add(timing.Item1);
			End = new DateTime(2014, 1, 1).Add(timing.Item2);
		}

		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public Room Room { get; set; }

		public Track Track { get; set; }

		public Speaker Speaker { get; set; }
	}
}