using System;

namespace DotNetRemoteWebDriver
{
    internal interface IDriverProcessMonitor : IDisposable
    {
        /// <summary>Looks for any newly started drivers and makes sure they're quitted when the application closes</summary>
        void MonitorNewDrivers();
    }
}