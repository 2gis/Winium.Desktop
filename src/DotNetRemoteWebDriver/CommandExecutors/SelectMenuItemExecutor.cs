#region using

using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class SelectMenuItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = ExecutedCommand.Parameters["ID"].ToString();
            var headersPath = ExecutedCommand.Parameters["PATH"].ToString();

            var menu = Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToMenu();

            try
            {
                menu.SelectItem(headersPath);
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