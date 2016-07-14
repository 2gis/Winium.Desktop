namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetTitleExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, Automator.Driver.Title);
        }
    }
}