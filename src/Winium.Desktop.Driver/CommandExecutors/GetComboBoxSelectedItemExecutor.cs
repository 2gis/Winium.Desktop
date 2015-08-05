namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Core;
    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetComboBoxSelectedItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var comboBox = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToComboBox();

            var selectedItemKey = this.Automator.ElementsRegistry.RegisterElement(comboBox.SelectedItem());
            var registeredObject = new JsonElementContent(selectedItemKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
