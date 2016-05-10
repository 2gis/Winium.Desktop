namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetElementTagNameExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, RequestedElement.TagName);
        }
    }
}