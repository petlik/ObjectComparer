using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
	public class ObjectComparatorSettings
	{
		public List<string> Ignore { get; set; }

		public ObjectComparatorSettings()
		{
			this.Ignore = new List<string>();
		}
	}
}
