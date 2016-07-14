namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ExecuteScriptExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var script = ExecutedCommand.Parameters["script"].ToString();
            Automator.Driver.ExecuteScript(script);
            return JsonResponse();
        }

    }
}