namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;
    using Winium.Desktop.Driver.Automator;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetSessionListExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            return this.JsonResponse(ResponseStatus.Success, Automator.Automators.ToDictionary(a => a.Session, a => a.ActualCapabilities));
        }

        #endregion
    }
}
