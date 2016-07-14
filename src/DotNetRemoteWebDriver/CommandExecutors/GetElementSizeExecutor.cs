using System.Collections.Generic;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetElementSizeExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var size = RequestedElement.Size;
            var response = new Dictionary<string, object>
            {
                {"width", size.Width},
                {"height", size.Height}
            };
            return JsonResponse(ResponseStatus.Success, response);
        }
    }
}