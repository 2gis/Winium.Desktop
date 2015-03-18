namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.StoreApps.Common;

    #endregion

    internal class CloseExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!this.Automator.Application.Close())
            {
                this.Automator.Application.Kill();
            }

            this.Automator.Elements.Clear();

            return this.JsonResponse(ResponseStatus.Success, null);
        }

        #endregion
    }
}
