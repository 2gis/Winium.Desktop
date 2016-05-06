﻿namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;

    #endregion

    internal class IsComboBoxExpandedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            return this.JsonResponse(ResponseStatus.Success, element.ToComboBox().IsExpanded);
        }

        #endregion
    }
}
