namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Extensions;

    #endregion

    internal class ScrollToDataGridCellExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(this.ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(this.ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = this.Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToDataGrid();

            dataGrid.ScrollTo(row, column);

            return this.JsonResponse();
        }

        #endregion
    }
}
