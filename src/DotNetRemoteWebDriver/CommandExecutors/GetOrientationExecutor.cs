#region using

using Winium.Cruciatus.Core;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class GetOrientationExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var orientation = RotationManager.GetCurrentOrientation();

            return JsonResponse(ResponseStatus.Success, orientation.ToString());
        }

        #endregion
    }
}