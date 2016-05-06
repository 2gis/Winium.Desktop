namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ElementEqualsExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();
            var otherRegisteredKey = ExecutedCommand.Parameters["other"].ToString();

            return JsonResponse(ResponseStatus.Success, registeredKey == otherRegisteredKey);
        }
    }
}