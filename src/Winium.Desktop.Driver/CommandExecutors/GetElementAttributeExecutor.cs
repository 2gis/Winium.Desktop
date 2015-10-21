namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using System.Windows.Automation;

    using Winium.Cruciatus.Extensions;
    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;

    #endregion

    internal class GetElementAttributeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var propertyName = this.ExecutedCommand.Parameters["NAME"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            try
            {
                var property = AutomationPropertyHelper.GetAutomationProperty(propertyName);
                var propertyObject = element.GetAutomationPropertyValue<object>(property);

                return this.JsonResponse(ResponseStatus.Success, SerializeObjectAsString(propertyObject));
            }
            catch (Exception)
            {
                return this.JsonResponse();
            }
        }

        /* Known types:
         * string, bool, int - easy to string
         * System.Window.Rect, System.Window.Point - overrides `ToString()` method
         * System.Windows.Automation.ControlType - should be used `ProgrammaticName` property
         */
        private static string SerializeObjectAsString(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var objAsControlType = obj as ControlType;
            return objAsControlType != null ? objAsControlType.ProgrammaticName : obj.ToString();
        }

        #endregion
    }
}
