namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus.Core;
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

            var x = Convert.ToInt32(this.ExecutedCommand.Parameters["x"]);
            var y = Convert.ToInt32(this.ExecutedCommand.Parameters["y"]);

            return TouchSimulator.TouchDown(x, y)
                ? this.JsonResponse()
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        #endregion
    }
}
