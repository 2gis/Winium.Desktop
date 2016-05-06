#region using

using DotNetRemoteWebDriver.Exceptions;
using DotNetRemoteWebDriver.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class FindChildElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var parentKey = ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = ExecutedCommand.Parameters["using"].ToString();

            var parent = Automator.ElementsRegistry.GetRegisteredElement(parentKey);
            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var element = parent.FindElement(strategy);
            if (element == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var registeredKey = Automator.ElementsRegistry.RegisterElement(element);
            var registeredObject = new JsonElementContent(registeredKey);
            return JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}