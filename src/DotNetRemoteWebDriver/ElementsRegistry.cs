using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace DotNetRemoteWebDriver
{
    internal class ElementsRegistry
    {
        private readonly Dictionary<string, IWebElement> _registeredWebElements = new Dictionary<string, IWebElement>();

        public void Clear()
        {
            _registeredWebElements.Clear();
        }
        
        public string Register(IWebElement element)
        {
            var key = Guid.NewGuid().ToString();
            _registeredWebElements.Add(key, element);
            return key;
        }

        public IWebElement Get(string registeredKey)
        {
            IWebElement element;
            if (!_registeredWebElements.TryGetValue(registeredKey, out element))
                throw new NotFoundException("No element with reference found: " + registeredKey);

            return element;
        }
    }
}