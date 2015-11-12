namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus.Core;
    using Winium.StoreApps.Common;

    #endregion
    
    internal class GetOrientationExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var orientation = RotationManager.GetCurrentOrientation();

            return this.JsonResponse(ResponseStatus.Success, orientation.ToString());
        }

        #endregion
    }
}