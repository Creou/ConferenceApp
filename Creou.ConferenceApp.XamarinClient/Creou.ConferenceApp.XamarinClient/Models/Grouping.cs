using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Creou.ConferenceApp.XamarinClient.Models
{
	public class Grouping<T> : ObservableCollection<T>
	{
		public string Key { get; set; }

		public string ShortKey { get; set; }

		public Grouping(string key, IEnumerable<T> items)
		{
			Key = key;
			ShortKey = key.Substring(6);
			foreach (var item in items)
			{
				Items.Add(item);
			}
		}
	}
}
