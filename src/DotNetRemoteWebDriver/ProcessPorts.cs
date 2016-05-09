using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetRemoteWebDriver
{
    /// <summary>
    /// Static class that returns the list of processes and the ports those processes use.
    /// </summary>
    public static class ProcessPorts
    {
        private struct PortUsage
        {
            public int Port { get; set; }
            public int ProcessId { get; set; }
        }

        public static bool TryFindProcessIdForPort(int port, out int processId)
        {
            processId = GetNetStatPorts().FirstOrDefault(p => p.Port == port).ProcessId;
            return processId != 0;
        }

        /// <summary>
        /// This method distills the output from netstat -a -n -o in order to 
        /// find which processes are using which ports
        /// </summary>
        private static IEnumerable<PortUsage> GetNetStatPorts()
        {

            using (var netstatProcess = new Process())
            {

                var startInfo = new ProcessStartInfo
                {
                    FileName = "netstat.exe",
                    Arguments = "-a -n -o",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                netstatProcess.StartInfo = startInfo;
                netstatProcess.Start();

                StreamReader standardOutput = netstatProcess.StandardOutput;
                StreamReader standardError = netstatProcess.StandardError;

                string netstatOutput = standardOutput.ReadToEnd() + standardError.ReadToEnd();

                if (netstatProcess.ExitCode != 0)
                    Console.WriteLine("NetStat command failed. This may require elevated permissions.");

                var outputLines = Regex.Split(netstatOutput, "\r\n");

                foreach (string line in outputLines)
                {
                    string[] tokens = Regex.Split(line, "\\s+");
                    if (tokens.Length > 4 && (tokens[1].Equals("UDP") || tokens[1].Equals("TCP")))
                    {
                        var ipAddress = Regex.Replace(tokens[2], @"\[(.*?)\]", "1.1.1.1");

                        {
                            var processId = tokens[1] == "UDP"
                                ? Convert.ToInt16(tokens[4])
                                : Convert.ToInt16(tokens[5]);

                            yield return new PortUsage
                            {
                                ProcessId = processId,
                                Port = Convert.ToInt32(ipAddress.Split(':')[1])
                            };
                        }
                    }
                }
            }
        }
    }
}
