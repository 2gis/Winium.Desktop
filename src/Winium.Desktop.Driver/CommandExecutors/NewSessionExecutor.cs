namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Threading;

    using Newtonsoft.Json;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Settings;
    using Winium.Desktop.Driver.Automator;
    using Winium.Desktop.Driver.Exceptions;
    using Winium.Desktop.Driver.Input;
    using Winium.StoreApps.Common;

    #endregion

    internal class NewSessionExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            try
            {
                this.Automator.Session = Guid.NewGuid().ToString();
                Logger.Debug("session for command: " + this.Automator.Session);

                // It is easier to reparse desired capabilities as JSON instead of re-mapping keys to attributes and calling type conversions, 
                // so we will take possible one time performance hit by serializing Dictionary and deserializing it as Capabilities object
                var serializedCapability =
                        JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);
                this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

                if (this.Automator.ActualCapabilities.ResetProcesses)
                {
                    this.ResetProcesses(this.Automator.ActualCapabilities.App);
                }
                this.ResetDirectory(this.Automator.ActualCapabilities.ResetDirectory);
                this.InitializeApplication(this.Automator.ActualCapabilities.DebugConnectToRunningApp);
                this.InitializeKeyboardEmulator(this.Automator.ActualCapabilities.KeyboardSimulator);

                // Gives sometime to load visuals (needed only in case of slow emulation)
                Thread.Sleep(this.Automator.ActualCapabilities.LaunchDelay);
            } catch(Exception e)
            {
                throw new SessionNotCreatedException(e.Message, e);
            }

            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
        }

        private void ResetProcesses(string app)
        {
            string executable = app.Substring(app.LastIndexOf("\\") + 1);
            string processName = executable.Substring(0, executable.LastIndexOf("."));
            foreach (Process process in Process.GetProcesses())
            {
                Logger.Debug("Running process: " + process.ProcessName);
            }
            foreach (Process process in Process.GetProcessesByName(processName))
            {
                Logger.Debug("Killing process: " + process.ProcessName);
                try
                {
                    process.Kill();
                }
                catch (Exception e)
                {
                    Logger.Info("Could not kill process: " + e.Message);
                }
                try
                {
                    process.WaitForExit(10000);
                } catch(Exception e)
                {
                    Logger.Info("Could not wait for process to exit: " + e.Message);
                }
            }
        }

        private void ResetDirectory(string resetDirectory)
        {
            if (!string.IsNullOrEmpty(resetDirectory))
            {
                DirectoryInfo directory = new DirectoryInfo(resetDirectory);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    subDirectory.Delete(true);
                }
            }
        }

        private void InitializeApplication(bool debugDoNotDeploy = false)
        {
            var appPath = this.Automator.ActualCapabilities.App;
            var appArguments = this.Automator.ActualCapabilities.Arguments;

            this.Automator.Application = new Application(appPath);
            if (!debugDoNotDeploy)
            {
                this.Automator.Application.Start(appArguments);
            }
        }

        private void InitializeKeyboardEmulator(KeyboardSimulatorType keyboardSimulatorType)
        {
            this.Automator.WiniumKeyboard = new WiniumKeyboard(keyboardSimulatorType);

            Logger.Debug("Current keyboard simulator: {0}", keyboardSimulatorType);
        }

        #endregion
    }
}
