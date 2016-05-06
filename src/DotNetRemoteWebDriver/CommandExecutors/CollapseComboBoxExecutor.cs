﻿namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;

    #endregion

    internal class CollapseComboBoxExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox().Collapse();

            return this.JsonResponse();
        }

        #endregion
    }
}
