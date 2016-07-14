namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetCurrentWindowHandleExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, Automator.Driver.CurrentWindowHandle);
        }
    }
}