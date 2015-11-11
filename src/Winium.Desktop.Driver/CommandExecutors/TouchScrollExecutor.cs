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

    internal class TouchScrollExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var haveElement = this.ExecutedCommand.Parameters.ContainsKey("element");
            var haveOffset = this.ExecutedCommand.Parameters.ContainsKey("xoffset")
                             && this.ExecutedCommand.Parameters.ContainsKey("yoffset");

            if (!haveOffset)
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var element = CruciatusFactory.Root;

            if (haveElement)
            {
                var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
                element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            }

            var rect = element.Properties.BoundingRectangle;

            var startPoint = new Point(
                rect.Left + (rect.Width / 2), 
                rect.Top + (rect.Height / 2));

            var xOffset = this.ExecutedCommand.GetParameterAsInt("xoffset");
            var yOffset = this.ExecutedCommand.GetParameterAsInt("yoffset");

            var endPoint = new Point(
                startPoint.X + xOffset,
                startPoint.Y + yOffset);

            if (!TouchSimulator.TouchDown((int)startPoint.X, (int)startPoint.Y))
            {
                return this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
            }

            var distance = Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2) + Math.Pow(startPoint.Y - endPoint.Y, 2));

            for (var soFar = 6; soFar < distance; soFar += 6)
            {
                var soFarFraction = soFar / distance;

                var x = (int)(startPoint.X + (xOffset * soFarFraction));
                var y = (int)(startPoint.Y + (yOffset * soFarFraction));
                if (!TouchSimulator.TouchUpdate(x, y))
                {
                    return this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
                }

                Thread.Sleep(8);
            }

            var startTime = DateTime.Now;
            while (DateTime.Now < (startTime + TimeSpan.FromMilliseconds(500)))
            {
                if (!TouchSimulator.TouchUpdate((int)endPoint.X, (int)endPoint.Y))
                {
                    return this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
                }
                Thread.Sleep(16);
            }

            return TouchSimulator.TouchUp((int)endPoint.X, (int)endPoint.Y)
                ? this.JsonResponse()
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        #endregion
    }
}