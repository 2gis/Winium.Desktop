namespace DotNetRemoteWebDriver.CommandExecutors
{

    #region using

    #endregion

    internal class GetExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var url = ExecutedCommand.Parameters["url"].ToString();
            Automator.Driver.Navigate().GoToUrl(url);
            return JsonResponse();
        }

        #endregion
    }
}