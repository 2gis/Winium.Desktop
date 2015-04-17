namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class IsElementDisplayedTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void IsDisplaydVisibleElement()
        {
            var element = this.MainWindow.FindElement(By.Id("TextBox1"));

            Assert.IsTrue(element.Displayed);
        }

        #endregion
    }
}
