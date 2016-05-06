namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ScreenshotExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var screenshot = Automator.Driver.GetScreenshot().AsBase64EncodedString;
            return JsonResponse(ResponseStatus.Success, screenshot);
        }
    }
}