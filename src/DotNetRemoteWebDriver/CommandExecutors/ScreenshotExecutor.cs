#region using

using Winium.Cruciatus;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class ScreenshotExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var screenshot = CruciatusFactory.Screenshoter.GetScreenshot();
            var screenshotSource = screenshot.AsBase64String();

            return JsonResponse(ResponseStatus.Success, screenshotSource);
        }

        #endregion
    }
}