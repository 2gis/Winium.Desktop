namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Linq;

    using Winium.Cruciatus;
    using Winium.Desktop.Driver.Input;

    #endregion

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var chars = this.ExecutedCommand.Parameters["value"].Select(x => Convert.ToChar(x.ToString()));

            WiniumKeyboard.GetInstance().SendKeys(chars.ToArray());

            return this.JsonResponse();
        }

        #endregion
    }
}
