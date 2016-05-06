#region using

using DotNetRemoteWebDriver.Extensions;
using Winium.Cruciatus.Elements;
using Winium.Cruciatus.Exceptions;
using Winium.Cruciatus.Extensions;

#endregion

namespace DotNetRemoteWebDriver.CommandExecutors
{
    #region using

    

    #endregion

    internal class ScrollToComboBoxItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = ExecutedCommand.Parameters["ID"].ToString();
            var searchStrategy = ExecutedCommand.Parameters["using"].ToString();
            var searchValue = ExecutedCommand.Parameters["value"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);

            var comboBox = Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToComboBox();

            CruciatusElement element;
            try
            {
                element = comboBox.ScrollTo(strategy);
            }
            catch (CruciatusException exception)
            {
                return JsonResponse(ResponseStatus.NoSuchElement, exception);
            }

            var elementKey = Automator.ElementsRegistry.RegisterElement(element);

            return JsonResponse(ResponseStatus.Success, new JsonElementContent(elementKey));
        }

        #endregion
    }
}