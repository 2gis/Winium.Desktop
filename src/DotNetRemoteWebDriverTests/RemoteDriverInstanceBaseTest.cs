using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DotNetRemoteWebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    public class RemoteDriverInstanceBaseTest
    {
        private DriverHost _driverService;

        [TestInitialize]
        public virtual void Initialize()
        {
            _driverService = new DriverHost(4444, null);
            Task.Run(() => _driverService.Run());
            WaitUntilStarted(_driverService);
        }

        private void WaitUntilStarted(DriverHost driverService)
        {
            Stopwatch time = Stopwatch.StartNew();
            while (time.Elapsed < TimeSpan.FromSeconds(15))
            {
                if (driverService.Running)
                    break;

                Thread.Sleep(200);
            }
            
            // Give it another second for good measure
            Thread.Sleep(2500);
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            _driverService?.Dispose();
        }
    }
}