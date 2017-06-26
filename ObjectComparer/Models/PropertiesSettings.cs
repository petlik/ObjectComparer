using ObjectComparer.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer.Results
{
	class PropertiesSettings
	{
		public string Name { get; set; }
		public Type Type { get; set; }
		public List<PropertiesParametersFlags> Flags { get; set; }
	}
}
