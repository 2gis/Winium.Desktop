namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ClickElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();
            Automator.ElementsRegistry.GetRegisteredElement(registeredKey).Click();

            return JsonResponse();
        }

        #endregion
    }
}