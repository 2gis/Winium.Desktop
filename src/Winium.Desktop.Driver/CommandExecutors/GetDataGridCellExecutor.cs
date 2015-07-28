namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetDataGridCellExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(this.ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(this.ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = this.Automator.Elements.GetRegisteredElement(registeredKey).ToDataGrid();

            var elementId = this.Automator.Elements.RegisterElement(dataGrid.Item(row, column));
            var webElement = new JsonWebElementContent(elementId);

            return this.JsonResponse(ResponseStatus.Success, webElement);
        }

        #endregion
    }
}
