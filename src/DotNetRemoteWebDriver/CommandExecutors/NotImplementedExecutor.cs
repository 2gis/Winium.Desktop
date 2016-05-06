#region using

using System;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class NotImplementedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var msg = string.Format("'{0}' is not valid or implemented command.", ExecutedCommand.Name);
            throw new NotImplementedException(msg);
        }

        #endregion
    }
}