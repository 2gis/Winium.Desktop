namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    using Winium.Cruciatus;

    #endregion

    internal class ScreenshotExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var screenshot = CruciatusFactory.Screenshoter.GetScreenshot();
            var screenshotSource = screenshot.AsBase64String();

            return this.JsonResponse(ResponseStatus.Success, screenshotSource);
        }

        #endregion
    }
}
