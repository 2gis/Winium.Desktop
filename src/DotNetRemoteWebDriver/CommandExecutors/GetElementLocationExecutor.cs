using System.Collections.Generic;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetElementLocationExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var location = RequestedElement.Location;
            var response = new Dictionary<string, object>
            {
                {"x", location.X},
                {"y", location.Y}
            };
            return JsonResponse(ResponseStatus.Success, response);
        }
    }
}
