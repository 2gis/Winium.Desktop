namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Threading;
    using System.Windows;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Core;
    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class TouchFlickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (this.ExecutedCommand.Parameters.ContainsKey("element"))
            {
                return FlickElement();
            }

            return Flick();
        }

        string Flick()
        { 
            if (!(this.ExecutedCommand.Parameters.ContainsKey("xspeed") 
                && this.ExecutedCommand.Parameters.ContainsKey("yspeed")))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var xSpeed = this.ExecutedCommand.GetParameterAsInt("xspeed");
            var ySpeed = this.ExecutedCommand.GetParameterAsInt("yspeed");

            return TouchSimulator.Flick(xSpeed, ySpeed)
                ? this.JsonResponse()
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        string FlickElement()
        {
            if (
                !(this.ExecutedCommand.Parameters.ContainsKey("element")
                  && this.ExecutedCommand.Parameters.ContainsKey("xoffset")
                  && this.ExecutedCommand.Parameters.ContainsKey("yoffset")
                  && this.ExecutedCommand.Parameters.ContainsKey("speed")))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var xOffset = this.ExecutedCommand.GetParameterAsInt("xoffset");
            var yOffset = this.ExecutedCommand.GetParameterAsInt("yoffset");

            var pixelsPerSecond = this.ExecutedCommand.GetParameterAsInt("speed");

            return TouchSimulator.FlickElement(element, xOffset, yOffset, pixelsPerSecond)
                ? this.JsonResponse() 
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        #endregion
    }
}