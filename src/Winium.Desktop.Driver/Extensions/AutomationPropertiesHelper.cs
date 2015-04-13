namespace Winium.Desktop.Driver.Extensions
{
    #region using

    using System.Windows.Automation;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Exceptions;

    #endregion

    public static class AutomationPropertiesHelper
    {
        #region Public Methods and Operators

        public static AutomationProperty GetAutomationProperty(string propertyName)
        {
            switch (propertyName)
            {
                case "IsControlElement":
                    return AutomationElementIdentifiers.IsControlElementProperty;
                case "ControlType":
                    return AutomationElementIdentifiers.ControlTypeProperty;
                case "IsContentElement":
                    return AutomationElementIdentifiers.IsContentElementProperty;
                case "LabeledBy":
                    return AutomationElementIdentifiers.LabeledByProperty;
                case "NativeWindowHandle":
                    return AutomationElementIdentifiers.NativeWindowHandleProperty;
                case "AutomationId":
                    return AutomationElementIdentifiers.AutomationIdProperty;
                case "ItemType":
                    return AutomationElementIdentifiers.ItemTypeProperty;
                case "IsPassword":
                    return AutomationElementIdentifiers.IsPasswordProperty;
                case "LocalizedControlType":
                    return AutomationElementIdentifiers.LocalizedControlTypeProperty;
                case "Name":
                    return AutomationElementIdentifiers.NameProperty;
                case "AcceleratorKey":
                    return AutomationElementIdentifiers.AcceleratorKeyProperty;
                case "AccessKey":
                    return AutomationElementIdentifiers.AccessKeyProperty;
                case "HasKeyboardFocus":
                    return AutomationElementIdentifiers.HasKeyboardFocusProperty;
                case "IsKeyboardFocusable":
                    return AutomationElementIdentifiers.IsKeyboardFocusableProperty;
                case "IsEnabled":
                    return AutomationElementIdentifiers.IsEnabledProperty;
                case "BoundingRectangle":
                    return AutomationElementIdentifiers.BoundingRectangleProperty;
                case "ProcessId":
                    return AutomationElementIdentifiers.ProcessIdProperty;
                case "RuntimeId":
                    return AutomationElementIdentifiers.RuntimeIdProperty;
                case "ClassName":
                    return AutomationElementIdentifiers.ClassNameProperty;
                case "HelpText":
                    return AutomationElementIdentifiers.HelpTextProperty;
                case "ClickablePoint":
                    return AutomationElementIdentifiers.ClickablePointProperty;
                case "Culture":
                    return AutomationElementIdentifiers.CultureProperty;
                case "IsOffscreen":
                    return AutomationElementIdentifiers.IsOffscreenProperty;
                case "Orientation":
                    return AutomationElementIdentifiers.OrientationProperty;
                case "FrameworkId":
                    return AutomationElementIdentifiers.FrameworkIdProperty;
                case "IsRequiredForForm":
                    return AutomationElementIdentifiers.IsRequiredForFormProperty;
                case "ItemStatus":
                    return AutomationElementIdentifiers.ItemStatusProperty;
                default:
                    CruciatusFactory.Logger.Error(
                        string.Format("Property {0} is not supported by CruciatusElement", propertyName));
                    throw new CruciatusException("UNSUPPORTED PROPERTY CALLED");
            }
        }

        #endregion
    }
}
