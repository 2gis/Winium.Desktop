#region using

using DotNetRemoteWebDriver.Exceptions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class FindMenuItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = ExecutedCommand.Parameters["ID"].ToString();
            var headersPath = ExecutedCommand.Parameters["PATH"].ToString();

            var munu = Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToMenu();

            var element = munu.GetItem(headersPath);
            if (element == null)
            {
                throw new AutomationException("No menu item was found", ResponseStatus.NoSuchElement);
            }

            var elementKey = Automator.ElementsRegistry.RegisterElement(element);

            return JsonResponse(ResponseStatus.Success, new JsonElementContent(elementKey));
        }

        #endregion
    }
}