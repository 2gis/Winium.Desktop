namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Core;
    using Winium.StoreApps.Common;

    #endregion

    internal class MouseUpExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            CruciatusFactory.Mouse.MouseUp();

            return this.JsonResponse();
        }

        #endregion
    }
}
