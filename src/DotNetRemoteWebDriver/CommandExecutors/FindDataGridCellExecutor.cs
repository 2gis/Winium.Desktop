#region using

using Winium.Cruciatus.Elements;
using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class FindDataGridCellExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = ExecutedCommand.Parameters["ID"].ToString();
            var column = int.Parse(ExecutedCommand.Parameters["COLUMN"].ToString());
            var row = int.Parse(ExecutedCommand.Parameters["ROW"].ToString());

            var dataGrid = Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToDataGrid();

            CruciatusElement dataGridCell;
            try
            {
                dataGridCell = dataGrid.Item(row, column);
            }
            catch (CruciatusException exception)
            {
                return JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            var registeredKey = Automator.ElementsRegistry.RegisterElement(dataGridCell);
            var registeredObject = new JsonElementContent(registeredKey);

            return JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}