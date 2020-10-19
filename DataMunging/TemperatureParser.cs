using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMunging
{
    public class TemperatureParser
    {
       static public DailyWeatherData ParseRow(ParsingProfile profile, string rowValue)
        {
            //DailyWeatherData
            var fields = rowValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int maxTemp = int.Parse(fields[profile.MaxIndex].Replace("*", ""));
            int minTemp = int.Parse(fields[profile.MinIndex].Replace("*", ""));


            return new DailyWeatherData() { Key = fields[profile.KeyIndex],
                                            MaxTemp = maxTemp,
                                            MinTemp = minTemp};
        }
        static public IEnumerable<DailyWeatherData> ParseFile(ParsingProfile profile)
        {

            var list = new List<DailyWeatherData>();

            foreach(var line  in profile.RowContent)
            {
                try
                {
                    list.Add(ParseRow(profile, line));
                }
                catch { }
            }
            return list;

        }

        public static string GetDayWithMinTemperatureSpread(IEnumerable<DailyWeatherData> parsedResults)
        {
            return parsedResults
                .OrderBy(r => r.DeltaTemp)
                .Select(r => r.Key)
                .First();
        }
    }
}
