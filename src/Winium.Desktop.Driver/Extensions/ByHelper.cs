namespace Winium.Desktop.Driver.Extensions
{
    #region using

    using System;
    using System.Windows.Automation;

    using Winium.Cruciatus.Core;

    #endregion

    public static class ByHelper
    {
        #region Public Methods and Operators

        public static By GetStrategy(string strategy, string value)
        {
            switch (strategy)
            {
                case "id":
                    return By.Uid(value);
                case "name":
                    return By.Name(value);
                case "class name":
                    return By.AutomationProperty(AutomationElementIdentifiers.ClassNameProperty, value);
                case "xpath":
                    return By.XPath(value);
                default:
                    throw new NotImplementedException(
                        string.Format("'{0}' is not valid or implemented searching strategy.", strategy));
            }
        }

        #endregion
    }
}
