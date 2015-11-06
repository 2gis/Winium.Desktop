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

            switch (result)
            {
                case 0:
                    return this.JsonResponse();
                case 1:
                    return this.JsonResponse(
                        ResponseStatus.UnableToSetDisplayOrientation,
                        "A device restart is required");
                case -2:
                    return this.JsonResponse(
                        ResponseStatus.UnableToSetDisplayOrientation,
                        this.ExecutedCommand.Parameters.ContainsKey("orientation") + " not supported by device");
                default:
                    return this.JsonResponse(
                        ResponseStatus.UnableToSetDisplayOrientation, "Unknown error: " + result);
            }
        }

        #endregion
    }
}