namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetCurrentUrlExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, Automator.Driver.Url);
        }
    }
}