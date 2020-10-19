using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataMunging
{
    public static class ParsingProfileFactory
    {
        public static ParsingProfile Create(string fileContents)
        {
            var lines = fileContents.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if(lines[0].TrimStart().StartsWith("Dy MxT   MnT"))
            {
                return new ParsingProfile
                {
                    KeyIndex = 0,
                    MaxIndex = 1,
                    MinIndex = 2,
                    RowContent = lines.Skip(1)
                };
            }
            else if(lines[0].TrimStart().StartsWith("Team"))
            {
                return new ParsingProfile
                {
                    KeyIndex = 1,
                    MaxIndex = 6,
                    MinIndex = 8,
                    RowContent = lines.Skip(1)//.SkipWhile(l => l.Contains("---"))
                }; ;
            }
            return null;
        }

    }
}
