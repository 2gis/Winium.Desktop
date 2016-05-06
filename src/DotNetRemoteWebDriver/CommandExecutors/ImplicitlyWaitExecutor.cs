#region using

using System;
using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class ImplicitlyWaitExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var timeout = ExecutedCommand.Parameters["ms"];

            CruciatusFactory.Settings.SearchTimeout = Convert.ToInt32(timeout);

            return JsonResponse();
        }

        #endregion
    }
}