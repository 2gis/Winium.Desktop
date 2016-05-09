using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetRemoteWebDriverTests
{
    [TestClass]
    [DeploymentItem("wires.exe")]
    [DeploymentItem("IEDriverServer.exe")]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("DotNetRemoteWebDriver.exe")]
    public class GlobalSetup
    {
        [AssemblyInitialize]
        public void Setup() { }
    }
}