namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus.Core;
    using Winium.Cruciatus.Elements;
    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class TouchLongPressExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var haveElement = this.ExecutedCommand.Parameters.ContainsKey("element");
            var havePoint = this.ExecutedCommand.Parameters.ContainsKey("x")
                            && this.ExecutedCommand.Parameters.ContainsKey("y");

            if (!(haveElement || havePoint))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            bool success;
            CruciatusElement element = null;
            var x = 0;
            var y = 0;

            if (haveElement)
            {
                var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
                element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            }

            if (havePoint)
            {
                x = this.ExecutedCommand.GetParameterAsInt("x");
                y = this.ExecutedCommand.GetParameterAsInt("y");
            }

            var duration = this.ExecutedCommand.Parameters.ContainsKey("duration")
                               ? this.ExecutedCommand.GetParameterAsInt("duration")
                               : 1000;

            if (haveElement && havePoint)
            {
                success = TouchSimulator.LongTap(element, x, y, duration);
            }
            else if (haveElement)
            {
                success = TouchSimulator.LongTap(element, duration);
            }
            else
            {
                success = TouchSimulator.LongTap(x, y, duration);
            }

            return success
                ? this.JsonResponse()
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        #endregion
    }
}