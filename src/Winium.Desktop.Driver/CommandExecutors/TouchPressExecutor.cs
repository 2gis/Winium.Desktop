namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus.Core;
    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class TouchPressExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!this.ExecutedCommand.Parameters.ContainsKey("x")
                && this.ExecutedCommand.Parameters.ContainsKey("y"))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var x = this.ExecutedCommand.GetParameterAsInt("x");
            var y = this.ExecutedCommand.GetParameterAsInt("y");

            bool success;

            if (this.ExecutedCommand.Parameters.ContainsKey("element"))
            {
                var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
                var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

                success = TouchSimulator.TouchDown(element, x, y);
            }
            else
            {
                success = TouchSimulator.TouchDown(x, y);
            }

            return success
                ? this.JsonResponse()
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        #endregion
    }
}
