namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetScreenshotExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            return JsonResponse(ResponseStatus.Success, Automator.Driver.GetScreenshot());
        }
    }
}
