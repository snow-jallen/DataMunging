using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace DataMunging.Test
{
    public class Tests
    {
        string [] data = new[]{
                " Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                "",
                "   8  75    54    65          50.0       0.00 FH      160  4.2 150  10  2.6  93 41 1026.3",
                "   9  86    32*   59       6  61.5       0.00         240  7.6 220  12  6.0  78 46 1018.6",
                "  12  88    73    81          68.7       0.00 RTH     250  8.1 270  21  7.9  94 51 1007.0",
                "  13  70    59    65          55.0       0.00 H       150  3.0 150   8 10.0  83 59 1012.6",
                "  14  61    59    60       5  55.9       0.00 RF      060  6.7 080   9 10.0  93 87 1008.6",
                };

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetLine()
        {
            var actualResult = TemperatureParser.ParseRow(data[2]);
            var expectedResult = makeWeatherData(8, 75, 54);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ParsingProperlyCleansLine()
        {
            var actualResult = TemperatureParser.ParseRow(data[3]);
            var expectedResult = makeWeatherData(9, 86, 32);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void TestGetThirdLine()
        {
            var actualResult = TemperatureParser.ParseRow(data[4]);
            var expectedResult = makeWeatherData(12, 88, 73);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [TestCase(8, 75, ExpectedResult = 75-8 )]
        [TestCase(-1, 75, ExpectedResult = 75-(-1) )]
        [TestCase(5, 75, ExpectedResult = 75-5 )]
        [TestCase(-17, -7, ExpectedResult = -7-(-17) )]
        public int TemperatureDeltaTest(int minTemp, int maxTemp) =>
            makeWeatherData(0, maxTemp, minTemp).DeltaTemp;

        static private DailyWeatherData makeWeatherData(int day, int maxTemp, int minTemp)
        {
            return new DailyWeatherData()
            {
                Day = day,
                MaxTemp = maxTemp,
                MinTemp = minTemp
            };
        }
    }
}