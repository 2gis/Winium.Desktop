namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class FindComboBoxSelectedItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var comboBox = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox();

            var selectedItem = comboBox.SelectedItem();
            if (selectedItem == null)
            {
                throw new AutomationException("No items is selected", ResponseStatus.NoSuchElement);
            }

            var selectedItemKey = this.Automator.ElementsRegistry.RegisterElement(selectedItem);
            var registeredObject = new JsonElementContent(selectedItemKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
