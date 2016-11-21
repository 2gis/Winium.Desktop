namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetPageSourceExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, Automator.Driver.PageSource);
        }
    }
}
