namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class GetExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var url = this.ExecutedCommand.Parameters["url"].ToString();
            this.Automator.Driver.Navigate().GoToUrl(url);
            return this.JsonResponse();
        }

        #endregion
    }
}
