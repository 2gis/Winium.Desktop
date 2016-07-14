namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class IsElementEnabledExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, RequestedElement.Enabled);
        }
    }
}