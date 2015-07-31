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
            var elementId = this.Automator.ElementsRegistry.RegisterElement(CruciatusFactory.FocusedElement);
            var webElement = new JsonWebElementContent(elementId);
            return this.JsonResponse(ResponseStatus.Success, webElement);
        }

        #endregion
    }
}
