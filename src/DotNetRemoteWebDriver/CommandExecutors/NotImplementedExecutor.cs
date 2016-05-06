using System;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class NotImplementedExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var msg = $"'{ExecutedCommand.Name}' is not valid or implemented command.";
            throw new NotImplementedException(msg);
        }
    }
}