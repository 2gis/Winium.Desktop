#region using

using System;
using System.Windows.Automation;
using DotNetRemoteWebDriver.Extensions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class GetElementAttributeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = ExecutedCommand.Parameters["ID"].ToString();
            var propertyName = ExecutedCommand.Parameters["NAME"].ToString();

            var element = Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            try
            {
                var property = AutomationPropertyHelper.GetAutomationProperty(propertyName);
                var propertyObject = element.GetAutomationPropertyValue<object>(property);

                return JsonResponse(ResponseStatus.Success, PrepareValueToSerialize(propertyObject));
            }
            catch (Exception)
            {
                return JsonResponse();
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

            return obj;
        }

        #endregion
    }
}