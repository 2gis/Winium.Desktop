#region using

using System;
using System.Linq;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class SendKeysToActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var chars = ExecutedCommand.Parameters["value"].Select(x => Convert.ToChar(x.ToString()));

            Automator.WiniumKeyboard.SendKeys(chars.ToArray());

            return JsonResponse();
        }

        #endregion
    }
}