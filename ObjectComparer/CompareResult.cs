using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
	public class CompareResult
	{
		public bool AreEqual { get; set; }
		public List<string> Differences { get; set; }
	}
}
