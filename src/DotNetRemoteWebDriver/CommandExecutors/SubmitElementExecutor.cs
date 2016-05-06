﻿#region using

using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class SubmitElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            CruciatusFactory.Keyboard.SendEnter();
            return JsonResponse();
        }

        #endregion
    }
}