namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetElementTextExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, RequestedElement.Text);
        }
    }
}