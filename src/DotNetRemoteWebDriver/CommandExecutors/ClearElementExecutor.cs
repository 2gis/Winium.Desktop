namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ClearElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            var element = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            element.SetText(null);

            return JsonResponse();
        }

        #endregion
    }
}