using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ObjectComparerTests")]
namespace ObjectComparer
{
    class CharacterComparer : BaseComparer, IComparer
    {
        private bool CaseInsensitive = false;

        #region Constructors
        public CharacterComparer() : base() { }
        public CharacterComparer(IEnumerable<ComparerFlags> flags) : base(flags)
        {
            this.CaseInsensitive = flags.Contains(ComparerFlags.CaseInsensitive);
        }
        #endregion

        private char GetCharacterValue(object A)
        {
            var charA = Convert.ToChar(A);
            
            if (this.CaseInsensitive)
            {
                return Char.ToLower(charA);
            }
            return charA;
        }

        public bool Compare(object A, object B)
        {
            var charA = this.GetCharacterValue(A);
            var charB = this.GetCharacterValue(B);
            
            return Char.Equals(charA, charB);
        }
    }
}
