namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Collections.Generic;

    using Winium.StoreApps.Common;

    #endregion

    internal class GetElementSizeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var boundingRect = element.Properties.BoundingRectangle;

            var response = new Dictionary<string, object>
                               {
                                   { "width", boundingRect.Width }, 
                                   { "height", boundingRect.Height }
                               };
            return this.JsonResponse(ResponseStatus.Success, response);
        }

        #endregion
    }
}
