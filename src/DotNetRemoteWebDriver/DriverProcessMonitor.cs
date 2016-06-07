using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace DotNetRemoteWebDriver
{
    internal class DriverProcessMonitor : IDriverProcessMonitor
    {
        private readonly List<int> _monitoredProcesses = new List<int>();

        public void Dispose()
        {            
        }

        public void MonitorChildren()
        {
            uint processHandle = (uint)Process.GetCurrentProcess().Handle;
            Logger.Debug($"Registering monitor on driver process handle {processHandle}.");
            MonitorAllChildren(processHandle);
        }

        private void MonitorAllChildren(uint parentProcessId)
        {
            var searcher = new ManagementObjectSearcher(
                "SELECT * " +
                "FROM Win32_Process " +
                "WHERE ParentProcessId=" + parentProcessId);

            var childProcesses = searcher.Get();
            if (childProcesses.Count <= 0) return;
            
            foreach (var item in childProcesses)
            {
                var childProcessId = (uint)item["ProcessId"];

                if (_monitoredProcesses.Contains((int) childProcessId))
                    continue;

                MonitorAllChildren(childProcessId);

                _monitoredProcesses.Add((int)childProcessId);
                Process process;
                if(!TryGetProcess(childProcessId, out process))
                    continue;

                ChildProcessTracker.AddProcess(process);
                _monitoredProcesses.Add((int)childProcessId);
            }
        }

        private bool TryGetProcess(uint childProcessId, out Process process)
        {
            process = Process.GetProcesses().FirstOrDefault(p => p.Id == childProcessId);
            return process != null;
        }

    }
}