namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SwitchToFrameExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var frameHandle = ExecutedCommand.Parameters["name"].ToString();
            Automator.Driver.SwitchTo().Frame(frameHandle);
            return JsonResponse();
        }
    }
}