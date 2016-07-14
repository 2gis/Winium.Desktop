namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SendKeysToElementExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();
            var text = string.Join(string.Empty, ExecutedCommand.Parameters["value"]);

            var element = Automator.ElementsRegistry.Get(registeredKey);
            element.SendKeys(text);

            return JsonResponse();
        }
    }
}