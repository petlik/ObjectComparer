using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Parameters
{
	public class ObjectComparatorParameters
	{
		public List<string> Ignore { get; set; }
		public List<PropertiesParameters> PropertiesParameters { get; set; }

		public ObjectComparatorParameters()
		{
			this.Ignore = new List<string>();
			this.PropertiesParameters = new List<PropertiesParameters>();
		}
	}
}
