#region using

using System.Windows.Automation;
using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class IsElementSelectedExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            var element = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            bool isSelected;

            try
            {
                var selectionItemProperty = SelectionItemPattern.IsSelectedProperty;
                isSelected = element.GetAutomationPropertyValue<bool>(selectionItemProperty);
            }
            catch (CruciatusException)
            {
                var toggleStateProperty = TogglePattern.ToggleStateProperty;
                var toggleState = element.GetAutomationPropertyValue<ToggleState>(toggleStateProperty);

                isSelected = toggleState == ToggleState.On;
            }

            return JsonResponse(ResponseStatus.Success, isSelected);
        }

        #endregion
    }
}