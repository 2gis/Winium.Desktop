namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class IsElementDisplayedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, RequestedElement.Displayed);
        }
    }
}