using System;
using System.Diagnostics;

namespace DotNetRemoteWebDriver
{
    /// <summary>Class that ensures the state is clear to run another instance of the remote web driver</summary>
    class PriorCleanup
    {
        public int? Port { get; set; }

        public void Run()
        {
            if (!Port.HasValue)
                throw new InvalidOperationException("The applicatio port must be defined before running.");

            int prevProcessId;
            if (!ProcessPorts.TryFindProcessIdForPort(Port.Value, out prevProcessId))
                return;

            Logger.Info($"Prior process {prevProcessId} using port {Port.Value} detected.");

            // Kill previous process and any kids
            Process process;
            if (ProcessTools.TryGetProcess(prevProcessId, out process))
            {
                Logger.Info($"Killing prior process {prevProcessId}..");
                process.KillAndWait();
            }

            foreach (var descendentId in ProcessTools.GetDescendents(prevProcessId))
            {
                if (!ProcessTools.TryGetProcess(descendentId, out process))
                    continue;

                Logger.Info($"Killing prior child process {descendentId}..");
                process.KillAndWait();
            }
        }
    }
}
