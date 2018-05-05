using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    class GenericComparer : IComparer
    {
        public bool Compare(object A, object B)
        {
            return Object.Equals(A, B);
        }
    }
}
