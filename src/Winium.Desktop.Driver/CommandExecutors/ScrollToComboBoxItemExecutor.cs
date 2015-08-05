namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;
    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class ScrollToComboBoxItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);

            var comboBox = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToComboBox();

            var element = comboBox.ScrollTo(strategy);

            var elementKey = this.Automator.ElementsRegistry.RegisterElement(element);

            return this.JsonResponse(ResponseStatus.Success, new JsonElementContent(elementKey));
        }

        #endregion
    }
}
