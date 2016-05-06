#region using

using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class ScrollToDataGridCellExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToDataGrid();

            try
            {
                dataGrid.ScrollTo(row, column);
            }
            catch (CruciatusException exception)
            {
                return JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            return JsonResponse();
        }

        #endregion
    }
}