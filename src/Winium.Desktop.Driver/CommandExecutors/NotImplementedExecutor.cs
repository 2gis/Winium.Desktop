namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    #endregion

    internal class NotImplementedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var msg = string.Format("'{0}' is not valid or implemented command.", this.ExecutedCommand.Name);
            throw new NotImplementedException(msg);
        }

        #endregion
    }
}
