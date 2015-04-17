namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class IsElementEnabledTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void IsDisabledElement()
        {
            var list = this.MainWindow.FindElement(By.Id("TextListBox"));

            var disabledCheckBox = this.MainWindow.FindElement(By.Id("CheckBox1"));
            disabledCheckBox.Click();

            Assert.IsFalse(list.Enabled);
        }

        [Test]
        public void IsEnabledElement()
        {
            var list = this.MainWindow.FindElement(By.Id("TextListBox"));

            Assert.IsTrue(list.Enabled);
        }

        #endregion
    }
}
