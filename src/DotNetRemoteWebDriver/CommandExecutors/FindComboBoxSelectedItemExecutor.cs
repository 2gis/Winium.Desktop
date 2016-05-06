#region using

using DotNetRemoteWebDriver.Exceptions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class FindComboBoxSelectedItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            var comboBox = Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox();

            var selectedItem = comboBox.SelectedItem();
            if (selectedItem == null)
            {
                throw new AutomationException("No items is selected", ResponseStatus.NoSuchElement);
            }

            var selectedItemKey = Automator.ElementsRegistry.RegisterElement(selectedItem);
            var registeredObject = new JsonElementContent(selectedItemKey);

            return JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}