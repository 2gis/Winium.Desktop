namespace WindowsFormsTestApplication.Tests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class IsElementSelectedTests : BaseTest
    {
        #region Fields

        private IWebElement mainWindow;

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void FindBaseElement()
        {
            this.mainWindow = this.Driver.FindElementById("Form1");
        }

        [Test]
        public void IsSelectedListItem()
        {
            var list = this.mainWindow.FindElement(By.Id("TextListBox"));
            var listItem = list.FindElement(By.Name("May"));

            listItem.Click();

            Assert.IsTrue(listItem.Selected);
        }

        [Test]
        public void IsSelectedTab()
        {
            var tab = this.mainWindow.FindElement(By.Name("TabItem1"));

            Assert.IsTrue(tab.Selected);
        }

        [Test]
        public void IsUnselectedListItem()
        {
            var list = this.mainWindow.FindElement(By.Id("TextListBox"));
            var listItem = list.FindElement(By.Name("May"));

            Assert.IsFalse(listItem.Selected);
        }

        [Test]
        public void IsUnselectedTab()
        {
            var tab = this.mainWindow.FindElement(By.Name("TabItem2"));

            Assert.IsFalse(tab.Selected);
        }

        #endregion
    }
}
