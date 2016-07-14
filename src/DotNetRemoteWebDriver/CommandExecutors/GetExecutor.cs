namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var url = ExecutedCommand.Parameters["url"].ToString();
            Automator.Driver.Navigate().GoToUrl(url);

            return JsonResponse();
        }
    }
}