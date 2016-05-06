namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SendKeysToElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();
            var text = string.Join(string.Empty, ExecutedCommand.Parameters["value"]);

            var element = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);
            element.SetText(text);

            return JsonResponse();
        }

        #endregion
    }
}