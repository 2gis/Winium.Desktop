namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SwitchToWindowExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var windowHandle = ExecutedCommand.Parameters["name"].ToString();
            Automator.Driver.SwitchTo().Window(windowHandle);
            return JsonResponse();
        }
    }
}