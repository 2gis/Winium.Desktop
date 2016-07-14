using System;

namespace DotNetRemoteWebDriver
{
    internal interface IDriverProcessMonitor : IDisposable
    {
        void MonitorChildren();
    }
}