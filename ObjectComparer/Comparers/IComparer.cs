using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    interface IComparer
    {
        bool Compare(object A, object B);
    }
}
