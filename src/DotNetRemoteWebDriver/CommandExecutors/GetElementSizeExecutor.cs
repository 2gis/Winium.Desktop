#region using

using System.Collections.Generic;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class GetElementSizeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();

            var element = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var boundingRect = element.Properties.BoundingRectangle;

            var response = new Dictionary<string, object>
            {
                {"width", boundingRect.Width},
                {"height", boundingRect.Height}
            };
            return JsonResponse(ResponseStatus.Success, response);
        }

        #endregion
    }
}