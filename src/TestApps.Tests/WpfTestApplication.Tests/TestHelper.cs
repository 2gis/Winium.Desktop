namespace WpfTestApplication.Tests
{
    #region using

    using System.Reflection;

    using OpenQA.Selenium;

    #endregion

    public static class TestHelper
    {
        #region Public Methods and Operators

        public static string GetElementId(IWebElement element)
        {
            return
                element.GetType()
                    .GetProperty("Id", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty)
                    .GetValue(element, null)
                    .ToString();
        }

        #endregion
    }
}
