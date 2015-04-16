namespace WindowsFormsTestApplication.Tests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class IsElementEnabledTests : BaseTest
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
        public void IsDisabledElement()
        {
            var list = this.mainWindow.FindElement(By.Id("TextListBox"));

            var disabledCheckBox = this.mainWindow.FindElement(By.Id("CheckBox1"));
            disabledCheckBox.Click();

            Assert.IsFalse(list.Enabled);
        }

        [Test]
        public void IsEnabledElement()
        {
            var list = this.mainWindow.FindElement(By.Id("TextListBox"));

            Assert.IsTrue(list.Enabled);
        }

        #endregion
    }
}
