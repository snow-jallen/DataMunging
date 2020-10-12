using System.Collections.Generic;

namespace DataMunging
{
    public class ParsingProfile
    {
        public int KeyIndex { get; set; }
        public int MaxIndex { get; set; }
        public int MinIndex { get; set; }
        public IEnumerable<string> RowContent { get; set; }

    }
}