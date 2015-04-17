namespace WpfTestApplication.Tests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class IsElementDisplayedTests : BaseTest
    {
        #region Public Methods and Operators

        [Test]
        public void IsDisplaydVisibleElement()
        {
            var element = this.Driver.FindElementById("WpfTestApplicationMainWindow").FindElement(By.Id("TextBox1"));

            Assert.IsTrue(element.Displayed);
        }

        #endregion
    }
}
