using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace DotNetRemoteWebDriver.CommandExecutors
{
    internal class Identifier
    {
        private static readonly Dictionary<string, Func<string, By>> Methods;

        static Identifier()
        {
            Methods = new Dictionary<string, Func<string, By>>
            {
                {"name", By.Name},
                {"tag name", By.TagName},
                {"class name", By.ClassName},
                {"css selector", By.CssSelector},
                {"id", By.Id},
                {"linktext", By.LinkText},
                {"partiallinktext", By.PartialLinkText},
                {"xpath", By.XPath}
            };
        }

        public static By From(IDictionary<string, JToken> parameters)
        {
            var searchValue = parameters["value"].ToString();
            var searchStrategy = parameters["using"].ToString().ToLower();

            Func<string, By> strategyFunc;
            if(!Methods.TryGetValue(searchStrategy, out strategyFunc))
                throw new NotSupportedException("Invalid or not supported finder strategy: " + searchStrategy);

            return strategyFunc(searchValue);
        }
    }
}