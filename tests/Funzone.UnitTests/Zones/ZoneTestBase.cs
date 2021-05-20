using Funzone.Domain.Zones;

namespace Funzone.UnitTests.Zones
{
    public class ZoneTestBase : TestBase
    {
        protected class ZoneTestDataOptions
        {
            internal Zone Zone { get; set; }
        }

        protected class ZoneTestData
        {
            public Zone Zone { get; }

            public ZoneTestData(Zone zone)
            {
                Zone = zone;
            }
        }
    }
}