using System;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class ImplicitlyWaitExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var timeout = int.Parse(ExecutedCommand.Parameters["ms"].ToString());
            Automator.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(timeout));
            return JsonResponse();
        }
    }
}