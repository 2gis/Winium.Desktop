using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    public class RemoteDriverInstanceBaseTest
    {
        private Process _driverProcess;

        [TestInitialize]
        public virtual void Initialize()
        {
            _driverProcess = Process.Start("DotNetRemoteWebDriver.exe");
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            _driverProcess?.Kill();
        }
    }
}