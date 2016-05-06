using System.Collections.Generic;
using DotNetRemoteWebDriver.CommandHelpers;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class StatusExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var response = new Dictionary<string, object> {{"build", new BuildInfo()}, {"os", new OSInfo()}};
            return JsonResponse(ResponseStatus.Success, response);
        }
    }
}