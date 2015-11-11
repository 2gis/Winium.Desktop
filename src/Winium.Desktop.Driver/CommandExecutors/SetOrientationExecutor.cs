namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;

    using Winium.StoreApps.Common;
    using Winium.Cruciatus.Core;

    #endregion using

    internal class SetOrientationExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!this.ExecutedCommand.Parameters.ContainsKey("orientation"))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return this.JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var orientation = (DisplayOrientation)Enum.Parse(
                typeof(DisplayOrientation), 
                this.ExecutedCommand.Parameters["orientation"].ToString());

            var result = RotationManager.SetOrientation(orientation);

            string message;

            switch (result)
            {
                case 0:
                    return this.JsonResponse();
                case 1:
                    message = "A device restart is required";
                    break;
                case -2:
                    message = this.ExecutedCommand.Parameters["orientation"] + " not supported by device";
                    break;
                default:
                    message = "Unknown error: " + result;
                    break;
            }

            Logger.Warn(message);
            return this.JsonResponse(ResponseStatus.UnknownError, message);
        }

        #endregion
    }
}