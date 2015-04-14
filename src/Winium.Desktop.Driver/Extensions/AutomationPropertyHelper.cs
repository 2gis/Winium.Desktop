namespace Winium.Desktop.Driver.Extensions
{
    #region using

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Automation;

    using Winium.Cruciatus;
    using Winium.Cruciatus.Exceptions;

    #endregion

    internal static class AutomationPropertyHelper
    {
        #region Static Fields

        private static readonly Dictionary<string, AutomationProperty> Properties;

        #endregion

        #region Constructors and Destructors

        static AutomationPropertyHelper()
        {
            Properties =
                typeof(AutomationElementIdentifiers).GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(AutomationProperty))
                    .ToDictionary(f => f.Name, f => (AutomationProperty)f.GetValue(null));
        }

        #endregion

        #region Public Methods and Operators

        internal static AutomationProperty GetAutomationProperty(string propertyName)
        {
            const string Suffix = "Property";
            var fullPropertyName = propertyName.EndsWith(Suffix) ? propertyName : propertyName + Suffix;
            if (Properties.ContainsKey(fullPropertyName))
            {
                return Properties[fullPropertyName];
            }

            CruciatusFactory.Logger.Error(string.Format("Property '{0}' is not UI Automation Property", propertyName));
            throw new CruciatusException("UNSUPPORTED PROPERTY");
        }

        #endregion
    }
}
