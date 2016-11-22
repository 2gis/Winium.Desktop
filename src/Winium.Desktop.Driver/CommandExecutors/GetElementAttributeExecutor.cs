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

                return this.JsonResponse(ResponseStatus.Success, PrepareValueToSerialize(propertyObject));
            }
            catch (Exception)
            {
                return this.JsonResponse();
            }
        }

        /* Known types:
         * string, bool, int - should be as plain text
         * System.Windows.Automation.ControlType - should be used `ProgrammaticName` property
         * System.Window.Rect, System.Window.Point - overrides `ToString()` method, can serialize
         */
        private static object PrepareValueToSerialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj.GetType().IsPrimitive)
            {
                return obj.ToString();
            }

            var controlType = obj as ControlType;
            if (controlType != null)
            {
                return controlType.ProgrammaticName;
            }

            // Fix issue error when parse runtimeId attribute of element(return Int32[] array) by convert them into string
            var intArray = obj as Int32[];
            if (intArray != null)
            {
                return string.Join(".", intArray);
            }

            // Fix issue error when parse Oriented attribute of element(return enum) by convert it to string
            var intValue = obj as Enum;
            if (intValue != null)
            {
                return intValue.ToString();
            }
            
            return obj;
        }

        #endregion
    }
}
