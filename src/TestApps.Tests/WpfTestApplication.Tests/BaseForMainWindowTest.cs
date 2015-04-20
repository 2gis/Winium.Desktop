namespace WpfTestApplication.Tests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class BaseForMainWindowTest : BaseTest
    {
        #region Public Properties

        public IWebElement MainWindow { get; set; }

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void FindMainWindow()
        {
            this.MainWindow = this.Driver.FindElementById("WpfTestApplicationMainWindow");
        }

        #endregion
    }
}
