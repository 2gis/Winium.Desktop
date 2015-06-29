namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Linq;

    #endregion

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var chars = this.ExecutedCommand.Parameters["value"].Select(x => Convert.ToChar(x.ToString()));

            this.Automator.WiniumKeyboard.SendKeys(chars.ToArray());

            return this.JsonResponse();
        }

        #endregion
    }
}
