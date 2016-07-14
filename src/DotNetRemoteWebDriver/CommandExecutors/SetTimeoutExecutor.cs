using System;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class SetTimeoutExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var type = ExecutedCommand.Parameters["type"].ToString();
            var timeout = int.Parse(ExecutedCommand.Parameters["ms"].ToString());

            if (type == "implicit")
                Automator.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(timeout));
            else
            {
                var msg = $"DotNetRemoteWebDriver does not implement timeout type '{type}'.";
                throw new NotImplementedException(msg);
            }

            return JsonResponse();
        }
    }
}