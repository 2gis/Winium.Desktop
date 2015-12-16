namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.Cruciatus;
    using Winium.StoreApps.Common;

    #endregion

    internal class SetTimeoutExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            string type = this.ExecutedCommand.Parameters["type"].ToString();
            var timeout = this.ExecutedCommand.Parameters["ms"];

            if (type == "implicit")
            {
                CruciatusFactory.Settings.SearchTimeout = Convert.ToInt32(timeout);
            }
            else
            {
                string msg = string.Format("Winium does not implement timeout type '{0}'.", type);
                throw new NotImplementedException(msg);
            }

            return this.JsonResponse();
        }

        #endregion
    }
}
