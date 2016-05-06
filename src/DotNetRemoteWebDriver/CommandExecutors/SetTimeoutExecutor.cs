#region using

using System;
using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class SetTimeoutExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var type = ExecutedCommand.Parameters["type"].ToString();
            var timeout = ExecutedCommand.Parameters["ms"];

            if (type == "implicit")
            {
                CruciatusFactory.Settings.SearchTimeout = Convert.ToInt32(timeout);
            }
            else
            {
                var msg = string.Format("DotNetRemoteWebDriver does not implement timeout type '{0}'.", type);
                throw new NotImplementedException(msg);
            }

            return JsonResponse();
        }

        #endregion
    }
}