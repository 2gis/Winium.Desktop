using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace DotNetRemoteWebDriver
{
    public static class ProcessTools
    {
        public static void KillAndWait(this Process process, TimeSpan timeout)
        {
            process.Kill();
            process.WaitForExit((int)timeout.TotalMilliseconds);
        }

        public static void KillAndWait(this Process process)
        {
            process.Kill();
            process.WaitForExit();
        }

        public static bool TryGetProcess(int processId, out Process process)
        {
            process = Process.GetProcesses().FirstOrDefault(p => p.Id == processId);
            return process != null;
        }

        public static IEnumerable<int> GetDescendents(int processId)
        {
            var searcher = new ManagementObjectSearcher(
                "SELECT * " +
                "FROM Win32_Process " +
                "WHERE ParentProcessId=" + processId);

            var childProcesses = searcher.Get();
            if (childProcesses.Count <= 0)
                yield break;

            foreach (var item in childProcesses)
            {
                var childProcessId = Convert.ToInt32(item["ProcessId"]);
                yield return childProcessId;
                foreach (var grandChildId in GetDescendents(childProcessId))
                    yield return grandChildId;
            }
        }
    }
}
