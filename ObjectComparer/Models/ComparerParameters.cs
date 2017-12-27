using System.Collections.Generic;

namespace ObjectComparer
{
    public class ComparerParameters
    {
        public List<string> Ignore { get; set; }
        public List<ComparerFlags> Flags;
        public List<ComparerProperties> Properties { get; set; }

        public ComparerParameters()
        {
            this.Ignore = new List<string>();
            this.Flags = new List<ComparerFlags>();
            this.Properties = new List<ComparerProperties>();
        }
    }
}
