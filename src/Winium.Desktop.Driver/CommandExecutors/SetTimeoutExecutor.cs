namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.StoreApps.Common;

    #endregion

    internal class SetTimeoutExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            return this.JsonResponse(ResponseStatus.Success, null);
        }

        #endregion
    }
}