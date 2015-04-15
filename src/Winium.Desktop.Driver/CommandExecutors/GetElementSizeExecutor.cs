namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Automation;

    using Winium.Cruciatus.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetElementSizeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.Elements.GetRegisteredElement(registeredKey);

            var property = AutomationElementIdentifiers.BoundingRectangleProperty;
            var boundingRect = element.GetAutomationPropertyValue<Rect>(property);

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
