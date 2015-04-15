namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Windows.Automation;

    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class IsElementDisplayedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.Elements.GetRegisteredElement(registeredKey);

            var property = AutomationElementIdentifiers.IsOffscreenProperty;
            var isDisplayd = ! element.GetAutomationPropertyValue<bool>(property);

            return this.JsonResponse(ResponseStatus.Success, isDisplayd);
        }

        #endregion
    }
}
