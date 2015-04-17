namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus;
    using Winium.StoreApps.Common;

    #endregion

    internal class ImplicitlyWaitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var timeout = this.ExecutedCommand.Parameters["ms"];

            CruciatusFactory.Settings.SearchTimeout = Convert.ToInt32(timeout);

            return this.JsonResponse();
        }

        #endregion
    }
}
