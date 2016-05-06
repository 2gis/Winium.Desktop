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

    internal class ScrollToListBoxItemExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var dataGridKey = ExecutedCommand.Parameters["ID"].ToString();
            var searchStrategy = ExecutedCommand.Parameters["using"].ToString();
            var searchValue = ExecutedCommand.Parameters["value"].ToString();

            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);

            var listBox = Automator.ElementsRegistry.GetRegisteredElement(dataGridKey).ToListBox();

            CruciatusElement element;
            try
            {
                element = listBox.ScrollTo(strategy);
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