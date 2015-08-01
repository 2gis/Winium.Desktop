namespace Winium.Desktop.Driver
{
    #region using

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Winium.Cruciatus.Elements;
    using Winium.StoreApps.Common;
    using Winium.StoreApps.Common.Exceptions;

    #endregion

    internal class ElementsRegistry
    {
        #region Static Fields

        private static int safeInstanceCount;

        #endregion

        #region Fields

        private readonly Dictionary<string, CruciatusElement> registeredElements;

        #endregion

        #region Constructors and Destructors

        public ElementsRegistry()
        {
            this.registeredElements = new Dictionary<string, CruciatusElement>();
        }

        #endregion

        #region Public Methods and Operators

        public void Clear()
        {
            this.registeredElements.Clear();
        }

        /// <summary>
        /// Returns CruciatusElement registered with specified key if any exists. Throws if no element is found.
        /// </summary>
        /// <exception cref="AutomationException">
        /// Registered element is not found or element has been garbage collected.
        /// </exception>
        public CruciatusElement GetRegisteredElement(string registeredKey)
        {
            var element = this.GetRegisteredElementOrNull(registeredKey);
            if (element != null)
            {
                return element;
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(CruciatusElement element)
        {
            var registeredKey =
                this.registeredElements.FirstOrDefault(
                    x => x.Value.Properties.RuntimeId == element.Properties.RuntimeId).Key;

            if (registeredKey == null)
            {
                Interlocked.Increment(ref safeInstanceCount);

                // TODO: Maybe use RuntimeId how registeredKey?
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

        #region Methods

        internal CruciatusElement GetRegisteredElementOrNull(string registeredKey)
        {
            CruciatusElement element;
            this.registeredElements.TryGetValue(registeredKey, out element);
            return element;
        }

        #endregion
    }
}
