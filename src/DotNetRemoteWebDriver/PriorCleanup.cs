using System;
using System.Diagnostics;

namespace DotNetRemoteWebDriver
{
    /// <summary>Class that ensures the state is clear to run another instance of the remote web driver</summary>
    class PriorCleanup
    {
        public PriorCleanup(int port)
        {
            if (port <= 1024)
                throw new ArgumentOutOfRangeException("The application port must be set greater than 1024.");

            Port = port;
        }

        private int Port { get; set; }

        public void Run()
        {
            Logger.Debug($"Clearing any prior processes listening on port {Port}.");
            int prevProcessId;
            if (!ProcessPorts.TryFindProcessIdForPort(Port, out prevProcessId))
            {
                Logger.Debug($"No prior process using port {Port} was found.");
                return;
            }

            Logger.Info($"Prior process {prevProcessId} using port {Port} detected.");

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

                Logger.Info($"Killing prior child process {descendentId}.");
                process.KillAndWait();
            }
        }
    }
}
