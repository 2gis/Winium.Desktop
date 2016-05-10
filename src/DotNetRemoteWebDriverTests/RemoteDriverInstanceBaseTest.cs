using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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
            var driverPath = Path.GetFullPath("DotNetRemoteWebDriver.exe");
            Console.WriteLine("Opening driver at: " + driverPath);
            _driverProcess = Process.Start(driverPath, "--log-path driver.log");
            
            // Give the driver a chance to start
            Thread.Sleep(1000);
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            _driverProcess?.Kill();
        }
    }
}