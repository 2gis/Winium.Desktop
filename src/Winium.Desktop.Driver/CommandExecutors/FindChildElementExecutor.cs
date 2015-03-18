namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.StoreApps.Common;

    #endregion

    internal class FindChildElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var elementId = this.Automator.Elements.FindElement(registeredKey, searchStrategy, searchValue);
            var webElement = new JsonWebElementContent(elementId);
            return this.JsonResponse(ResponseStatus.Success, webElement);
        }

        #endregion
    }
}
