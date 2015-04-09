namespace Winium.Desktop.Driver
{
    #region using

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Winium.Cruciatus.Elements;
    using Winium.Desktop.Driver.Extensions;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class ElementStorage
    {
        #region Static Fields

        private static int safeInstanceCount;

        #endregion

        #region Fields

        private readonly Dictionary<string, CruciatusElement> registeredElements;

        #endregion

        #region Constructors and Destructors

        public ElementStorage()
        {
            this.registeredElements = new Dictionary<string, CruciatusElement>();
        }

        #endregion

        #region Public Methods and Operators

        public void Clear()
        {
            this.registeredElements.Clear();
        }

        public string FindElement(CruciatusElement parent, string searchStrategy, string searchValue)
        {
            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var element = parent.FindElement(strategy);
            if (element == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            return this.RegisterElement(element);
        }

        public string FindElement(string parentRegisteredKey, string searchStrategy, string searchValue)
        {
            var parent = this.GetRegisteredElement(parentRegisteredKey);
            return this.FindElement(parent, searchStrategy, searchValue);
        }

        public IEnumerable<string> FindElements(CruciatusElement parent, string searchStrategy, string searchValue)
        {
            var strategy = ByHelper.GetStrategy(searchStrategy, searchValue);
            var elements = parent.FindElements(strategy);
            if (elements == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            return this.RegisterElements(elements);
        }

        public IEnumerable<string> FindElements(string parentRegisteredKey, string searchStrategy, string searchValue)
        {
            var parent = this.GetRegisteredElement(parentRegisteredKey);
            return this.FindElements(parent, searchStrategy, searchValue);
        }

        /// <summary>
        /// Returns FrameworkElement registered with specified key if any exists. Throws if no element is found.
        /// </summary>
        /// <exception cref="AutomationException">
        /// Registered element is not found or element has been garbage collected.
        /// </exception>
        public CruciatusElement GetRegisteredElement(string registeredKey)
        {
            CruciatusElement element;
            if (this.registeredElements.TryGetValue(registeredKey, out element))
            {
                if (element != null)
                {
                    return element;
                }
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(CruciatusElement element)
        {
            var registeredKey = this.registeredElements.FirstOrDefault(x => x.Value == element).Key;

            if (registeredKey == null)
            {
                Interlocked.Increment(ref safeInstanceCount);

                registeredKey = element.GetHashCode() + "-"
                                + safeInstanceCount.ToString(string.Empty, CultureInfo.InvariantCulture);
                this.registeredElements.Add(registeredKey, element);
            }

            return registeredKey;
        }

        public IEnumerable<string> RegisterElements(IEnumerable<CruciatusElement> elements)
        {
            return elements.Select(this.RegisterElement);
        }

        #endregion
    }
}
