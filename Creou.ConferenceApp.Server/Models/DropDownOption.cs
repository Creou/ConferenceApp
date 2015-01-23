using System.Collections.Generic;

namespace Creou.ConferenceApp.Server.Models
{
	public class DropDownOptionValue
	{
		public int Id { get; set; }

		public DropDownOption Option { get; set; }

		public string Text { get; set; }
	}

	public class DropDownOption
	{
		public int Id { get; set; }

		public string OptionName { get; set; }

		public Conference Conference { get; set; }

		public virtual ICollection<DropDownOptionValue> Values { get; set; }
	}

	public class Conference
	{
		public int Id { get; set; }

		public string ConferenceName { get; set; }
	}
}