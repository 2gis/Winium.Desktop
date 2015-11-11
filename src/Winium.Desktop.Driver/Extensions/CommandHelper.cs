namespace Winium.Desktop.Driver.Extensions
{
    #region using

    using System;

    using Winium.StoreApps.Common;

    #endregion

    public static class CommandHelper
    {
        public static int GetParameterAsInt(this Command command, string propertyName)
        {
            return (int)Math.Round(Convert.ToDouble(command.Parameters[propertyName]));
        }
    }
}
