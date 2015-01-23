using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Creou.ConferenceApp.Server.Models
{
	public class CreouConferenceAppServerContext : DbContext
	{
		// You can add custom code to this file. Changes will not be overwritten.
		// 
		// If you want Entity Framework to drop and regenerate your database
		// automatically whenever you change your model schema, please use data migrations.
		// For more information refer to the documentation:
		// http://msdn.microsoft.com/en-us/data/jj591621.aspx

		public CreouConferenceAppServerContext()
			: base("name=CreouConferenceAppServerContext")
		{
			Database.SetInitializer<CreouConferenceAppServerContext>(new CreouConferenceAppDBInitializer());
		}

		public DbSet<Session> Sessions { get; set; }

		public DbSet<Room> Rooms { get; set; }

		public DbSet<Speaker> Speakers { get; set; }

		public DbSet<Track> Tracks { get; set; }

		public DbSet<Attendee> Attendees { get; set; }

		public DbSet<FeedbackReport> FeedbackReports { get; set; }
		
		public DbSet<EventFeedbackReport> EventFeedbackReports { get; set; }
		
		public DbSet<DropDownOption> DropDownOptions { get; set; }

		public DbSet<DropDownOptionValue> DropDownOptionValues { get; set; }

		public DbSet<Conference> Conferences { get; set; }
	}
}