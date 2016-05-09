using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading;

namespace DotNetRemoteWebDriver
{
    internal class DriverProcessMonitor : IDriverProcessMonitor
    {
        private readonly List<uint> _monitoredProcesses = new List<uint>();
        private readonly JobObject _jobObject = new JobObject();

        public void MonitorNewDrivers()
        {
            MonitorProcessesCreatedBy((uint) Process.GetCurrentProcess().Id);
            new Timer(Callback, null, 500, 500);
        }

        private void Callback(object state)
        {
            MonitorNewDrivers();
        }

        private void MonitorProcessesCreatedBy(uint parentProcessId)
        {
            var processSearch = new ManagementObjectSearcher(
                "SELECT * " +
                "FROM Win32_Process " +
                "WHERE ParentProcessId=" + parentProcessId);

            var childProcesses = processSearch.Get();
            if (childProcesses.Count <= 0)
                return;

            foreach (var item in childProcesses)
            {
                var childProcessId = (uint)item["ProcessId"];
                if (childProcessId == Process.GetCurrentProcess().Id)
                    continue;

                MonitorProcessesCreatedBy(childProcessId);
                if (_monitoredProcesses.Contains(childProcessId))
                    continue;
                    
                Logger.Info("Registered sub process for shutdown on process exit: " + childProcessId);
                _jobObject.AddProcess((int)childProcessId);
                _monitoredProcesses.Add(childProcessId);
            }
        }

        public void Dispose()
        {
            _jobObject.Dispose();
        }
    }
}