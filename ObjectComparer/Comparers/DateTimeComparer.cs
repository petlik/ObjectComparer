using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectComparer
{
    class DateTimeComparer : BaseComparer, IComparer
    {
        private bool IgnoreTime;

        #region Constructors
        public DateTimeComparer() : base() { }
        public DateTimeComparer(IEnumerable<ComparerFlags> flags) : base(flags)
        {
            this.IgnoreTime = flags.Contains(ComparerFlags.IgnoreTime);
        }
        #endregion

        private DateTime GetDateTimeValue(object A) {
            var value = A as DateTime? ?? new DateTime();

            if (this.IgnoreTime)
                return value.Date;

            return value;
        }
        public bool Compare(object A, object B)
        {
            var dateTimeA = this.GetDateTimeValue(A);
            var dateTimeB = this.GetDateTimeValue(B);
            
            return DateTime.Compare(dateTimeA, dateTimeB) == 0;
        }
    }
}
