namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Windows.Automation;

    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class IsElementSelectedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.Elements.GetRegisteredElement(registeredKey);

            var isSelected = false;

            var isPatternAvailable = AutomationElementIdentifiers.IsSelectionItemPatternAvailableProperty;
            if (element.GetAutomationPropertyValue<bool>(isPatternAvailable))
            {
                var property = SelectionItemPattern.IsSelectedProperty;
                isSelected = element.GetAutomationPropertyValue<bool>(property);
            }

            return this.JsonResponse(ResponseStatus.Success, isSelected);
        }

        #endregion
    }
}
