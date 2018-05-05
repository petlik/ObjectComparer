using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    abstract class BaseComparer
    {
        public IEnumerable<ComparerFlags> Flags { get; set; }

        public BaseComparer()
        {
            this.Flags = new List<ComparerFlags>();
        }

        public BaseComparer(IEnumerable<ComparerFlags> flags)
        {
            this.Flags = flags;
        }
    }
}
