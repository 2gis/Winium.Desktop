namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Core;
    using Winium.StoreApps.Common;

    #endregion

    internal class TouchSingleTapExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!ExecutedCommand.Parameters.ContainsKey("element"))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var registeredKey = this.ExecutedCommand.Parameters["element"].ToString();
            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            return TouchSimulator.Tap(element)
                ? this.JsonResponse()
                : this.JsonResponse(ResponseStatus.UnknownError, "Touch input failed");
        }

        #endregion
    }
}
