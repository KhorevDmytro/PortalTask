using NUnit.Framework;
using PortalTask.Requests;

namespace PortalTask.Tests
{
    public class _BaseTest
    {
        public SystemRequests _systemRequests;

        [SetUp]
        public void BeforeTest()
        {
            _systemRequests = new SystemRequests();
        }
    }
}