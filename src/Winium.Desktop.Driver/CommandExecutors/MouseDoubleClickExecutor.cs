namespace Winium.Desktop.Driver.CommandExecutors
{
    #region using

    using Winium.Cruciatus;
    using Winium.Cruciatus.Core;

    #endregion

    internal class MouseDoubleClickExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            CruciatusFactory.Mouse.DoubleClick(MouseButton.Left);
            return this.JsonResponse();
        }

        #endregion
    }
}
