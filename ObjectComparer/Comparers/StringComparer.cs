using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ObjectComparerTests")]
namespace ObjectComparer
{
    class StringComparer : BaseComparer, IComparer
    {
        private bool CaseInsensitive = false;

        #region Constructors
        public StringComparer() : base() { }
        public StringComparer(IEnumerable<ComparerFlags> flags) : base(flags)
        {
            this.CaseInsensitive = flags.Contains(ComparerFlags.CaseInsensitive);
        }
        #endregion

        private string GetStringValue(object A)
        {
            var stringA = A as string;
            if (this.CaseInsensitive) {
                return stringA.ToLower();
            }
            return stringA;
        }

        public bool Compare(object A, object B)
        {
            var stringA = this.GetStringValue(A);
            var stringB = this.GetStringValue(B);

            if (stringA.Length != stringB.Length)
                return false;

            return String.Equals(stringA, stringB);
        }
    }
}
