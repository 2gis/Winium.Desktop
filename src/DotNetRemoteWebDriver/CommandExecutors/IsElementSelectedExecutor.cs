namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class IsElementSelectedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, RequestedElement.Selected);
        }
    }
}