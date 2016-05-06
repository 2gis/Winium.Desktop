#region using

using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class GetDataGridRowCountExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            var dataGrid = Automator.ElementsRegistry.GetRegisteredElement(registeredKey).ToDataGrid();

            return JsonResponse(ResponseStatus.Success, dataGrid.RowCount);
        }

        #endregion
    }
}