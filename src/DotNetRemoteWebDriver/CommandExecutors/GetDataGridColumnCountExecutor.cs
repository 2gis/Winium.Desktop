namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;

    #endregion

    internal class GetDataGridColumnCountExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToDataGrid();

            return this.JsonResponse(ResponseStatus.Success, dataGrid.ColumnCount);
        }

        #endregion
    }
}
