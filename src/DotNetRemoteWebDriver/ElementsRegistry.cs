#region using

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using DotNetRemoteWebDriver.Exceptions;
using Winium.Cruciatus.Elements;

#endregion

namespace DotNetRemoteWebDriver
{
    #region using

    

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
            registeredElements = new Dictionary<string, CruciatusElement>();
        }

        #endregion

        #region Methods

        internal CruciatusElement GetRegisteredElementOrNull(string registeredKey)
        {
            CruciatusElement element;
            registeredElements.TryGetValue(registeredKey, out element);
            return element;
        }

        #endregion

        #region Public Methods and Operators

        public void Clear()
        {
            registeredElements.Clear();
        }

        /// <summary>
        ///     Returns CruciatusElement registered with specified key if any exists. Throws if no element is found.
        /// </summary>
        /// <exception cref="AutomationException">
        ///     Registered element is not found or element has been garbage collected.
        /// </exception>
        public CruciatusElement GetRegisteredElement(string registeredKey)
        {
            var element = GetRegisteredElementOrNull(registeredKey);
            if (element != null)
            {
                return element;
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(CruciatusElement element)
        {
            var registeredKey =
                registeredElements.FirstOrDefault(
                    x => x.Value.Properties.RuntimeId == element.Properties.RuntimeId).Key;

            if (registeredKey == null)
            {
                Interlocked.Increment(ref safeInstanceCount);

                // TODO: Maybe use RuntimeId how registeredKey?
                registeredKey = element.GetHashCode() + "-"
                                + safeInstanceCount.ToString(string.Empty, CultureInfo.InvariantCulture);
                registeredElements.Add(registeredKey, element);
            }

            return registeredKey;
        }

        public IEnumerable<string> RegisterElements(IEnumerable<CruciatusElement> elements)
        {
            return elements.Select(RegisterElement);
        }

        #endregion
    }
}