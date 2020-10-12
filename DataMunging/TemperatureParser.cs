using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMunging
{
    public class TemperatureParser
    {
        public TemperatureParser(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        private IDataProvider dataProvider;

       static public DailyWeatherData ParseRow(string rowValue)
        {
            //DailyWeatherData
            var fields = rowValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (int.TryParse(fields[0], out int day) == false)
                throw new ApplicationException("Unparsable data");
            int maxTemp = int.Parse(fields[1].Replace("*", ""));
            int minTemp = int.Parse(fields[2].Replace("*", ""));


            return new DailyWeatherData() { Day = day,
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
                    list.Add(ParseRow(line));
                }
                catch { }
            }
            return list;

        }

        public static int GetDayWithMinTemperatureSpread(IEnumerable<DailyWeatherData> parsedResults)
        {
            return parsedResults
                .OrderBy(r => r.DeltaTemp)
                .Select(r => r.Day)
                .First();
        }
    }
}
