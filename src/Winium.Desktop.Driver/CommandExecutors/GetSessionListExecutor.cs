namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;
    using Winium.StoreApps.Common;
    using Winium.Desktop.Driver.Automator;

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
