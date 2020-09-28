using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace DataMunging.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetLine()
        {
            var mock = new Mock<IDataProvider>();
            //do something here that gets the info we need it to get to do the things we need it to do to pass this test that we still need to write
            var data = new[]{
                " Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                "",
                "   8  75    54    65          50.0       0.00 FH      160  4.2 150  10  2.6  93 41 1026.3",
                "   9  86    32*   59       6  61.5       0.00         240  7.6 220  12  6.0  78 46 1018.6",
                "  12  88    73    81          68.7       0.00 RTH     250  8.1 270  21  7.9  94 51 1007.0",
                "  13  70    59    65          55.0       0.00 H       150  3.0 150   8 10.0  83 59 1012.6",
                "  14  61    59    60       5  55.9       0.00 RF      060  6.7 080   9 10.0  93 87 1008.6",
                };
            //mock.Setup(m => m.ReadLine()).Returns(data[rowNum]);

            var parser = new TemperatureParser(mock.Object);

            var actualResult = parser.ParseRow(data[2]);

            var expectedResult = new DailyWeatherData()
            {
                Day = 8,
                MaxTemp = 75,
                MinTemp = 54
            };

            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
}