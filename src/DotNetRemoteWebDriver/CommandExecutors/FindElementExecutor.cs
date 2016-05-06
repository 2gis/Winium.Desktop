#region using

using DotNetRemoteWebDriver.Exceptions;
using DotNetRemoteWebDriver.Extensions;
using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class FindElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var searchValue = ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = ExecutedCommand.Parameters["using"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var element = CruciatusFactory.Root.FindElement(strategy);
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