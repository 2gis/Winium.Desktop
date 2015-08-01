namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetActiveElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.Automator.ElementsRegistry.RegisterElement(CruciatusFactory.FocusedElement);
            var registeredObject = new JsonElementContent(registeredKey);
            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
