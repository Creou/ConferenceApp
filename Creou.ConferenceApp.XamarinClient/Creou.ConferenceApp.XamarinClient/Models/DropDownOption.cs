using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creou.ConferenceApp.XamarinClient.Models
{
	public class DropDownOption
	{
		public int Id { get; set; }

		public string OptionName { get; set; }

		public Conference Conference { get; set; }

		public virtual ICollection<DropDownOptionValue> Values { get; set; }
	}
}
