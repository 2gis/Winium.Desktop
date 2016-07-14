using System;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class GetElementAttributeExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var propertyName = ExecutedCommand.Parameters["NAME"].ToString();
            try
            {
                var value = RequestedElement.GetAttribute(propertyName);
                return JsonResponse(ResponseStatus.Success, value);
            }
            catch (Exception e)
            {
                return JsonResponse(ResponseStatus.InvalidElementState, $"Failed to get property '{propertyName}': {e.Message}");
            }
        }
    }
}