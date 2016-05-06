#region using

using System;
using Winium.Cruciatus.Core;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion using

    internal class SetOrientationExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            if (!ExecutedCommand.Parameters.ContainsKey("orientation"))
            {
                // TODO: in the future '400 : invalid argument' will be used
                return JsonResponse(ResponseStatus.UnknownError, "WRONG PARAMETERS");
            }

            var orientation =
                (DisplayOrientation)
                    Enum.Parse(typeof (DisplayOrientation), ExecutedCommand.Parameters["orientation"].ToString());

            var result = RotationManager.SetOrientation(orientation);

            string message;

            switch (result)
            {
                case 0:
                    return JsonResponse();
                case 1:
                    message = "A device restart is required";
                    break;
                case -2:
                    message = ExecutedCommand.Parameters["orientation"] + " not supported by device";
                    break;
                default:
                    message = "Unknown error: " + result;
                    break;
            }

            Logger.Warn(message);
            return JsonResponse(ResponseStatus.UnknownError, message);
        }

        #endregion
    }
}