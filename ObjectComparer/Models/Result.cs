using System.Collections.Generic;

namespace ObjectComparer
{
    public class Result
    {
        public bool AreEqual { get; set; }
        public List<string> Differences { get; set; }
    }
}
