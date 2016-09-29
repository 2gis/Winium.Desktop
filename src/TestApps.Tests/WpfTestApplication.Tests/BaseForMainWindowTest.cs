namespace WpfTestApplication.Tests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    public class BaseForMainWindowTest : BaseTest<RemoteWebDriver>
    {
        #region Public Properties

        public IWebElement MainWindow { get; set; }

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void FindMainWindow()
        {
            this.MainWindow = this.Driver.FindElement(By.XPath("/*[@AutomationId='WpfTestApplicationMainWindow']"));
        }

        #endregion
    }
}
