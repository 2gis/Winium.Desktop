namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Linq;

    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class FindChildElementsExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var elements = parent.FindElements(strategy);

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(elements);
            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));
            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

        #endregion
    }
}
