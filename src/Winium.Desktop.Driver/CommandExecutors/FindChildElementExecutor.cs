namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class FindChildElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var parentKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automator.ElementsRegistry.GetRegisteredElement(parentKey);
            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var element = parent.FindElement(strategy);
            if (element == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(element);
            var registeredObject = new JsonElementContent(registeredKey);
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
