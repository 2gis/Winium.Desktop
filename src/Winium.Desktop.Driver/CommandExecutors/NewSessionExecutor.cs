﻿namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using
    using System;
    using System.Threading;

    using Newtonsoft.Json;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Settings;
    using Winium.Desktop.Driver.Automator;
    using Winium.Desktop.Driver.Input;
    using Winium.StoreApps.Common;

    #endregion

    internal class NewSessionExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            // It is easier to reparse desired capabilities as JSON instead of re-mapping keys to attributes and calling type conversions, 
            // so we will take possible one time performance hit by serializing Dictionary and deserializing it as Capabilities object
            var serializedCapability =
                JsonConvert.SerializeObject(this.ExecutedCommand.Parameters["desiredCapabilities"]);            
            this.Automator.ActualCapabilities = Capabilities.CapabilitiesFromJsonString(serializedCapability);

            this.InitializeApplication(this.Automator.ActualCapabilities.DebugConnectToRunningApp);
            this.InitializeKeyboardEmulator(this.Automator.ActualCapabilities.KeyboardSimulator);

            // Gives sometime to load visuals (needed only in case of slow emulation)
            Thread.Sleep(this.Automator.ActualCapabilities.LaunchDelay);

            // Update running application process in Application class if it's exited base on input process name
            if (this.Automator.Application.HasExited())
            {
                // Add parse process name pass from request
                var processName = (this.ExecutedCommand.Parameters["desiredCapabilities"]["processname"]);
                // Update launched process by process name if it's exited
                if (processName != null)
                {
                    this.Automator.Application.UpdateRunApplicationProcessBy(processName.ToString());
                }
            }
            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
            //if (this.Automator.Application.HasExited())
            //{
            //    try
            //    {
            //        // Add parse process name pass from request
            //        var processName = (this.ExecutedCommand.Parameters["desiredCapabilities"]["processname"]).ToString();
            //        //if (processName != null && processName != string.Empty)
            //        //{
            //            // Update launched process by process name if it's exited
            //            var result = this.Automator.Application.UpdateRunApplicationProcessBy(processName);
            //            if (!result)
            //            {
            //                return this.JsonResponse(ResponseStatus.UnableToGetLaunchedApplication, this.Automator.ActualCapabilities);
            //            }
            //        //}
            //    }
            //    catch (Exception)
            //    {
            //        return this.JsonResponse();
            //    }
            //}
            //return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);

            //if (!this.Automator.Application.HasExited())
            //{
            //    return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
            //}
            //else
            //{
            //    try
            //    {
            //        // Add parse process name pass from request
            //        var processName = (this.ExecutedCommand.Parameters["desiredCapabilities"]["processname"]).ToString();

            //        // Update launched process by process name if it's exited
            //        var result = this.Automator.Application.UpdateRunApplicationProcessBy(processName);
            //        if (result)
            //        {
            //            return this.JsonResponse(ResponseStatus.Success, this.Automator.ActualCapabilities);
            //        }
            //        return this.JsonResponse(ResponseStatus.UnableToGetLaunchedApplication, this.Automator.ActualCapabilities);
            //    }
            //    catch (Exception)
            //    {
            //        return this.JsonResponse();
            //    }
            //}
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
