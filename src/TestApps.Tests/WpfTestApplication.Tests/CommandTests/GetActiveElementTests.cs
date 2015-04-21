namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class GetActiveElementTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void GetActiveElement()
        {
            var textBox = this.MainWindow.FindElement(By.Id("TextBox1"));
            textBox.Click();

            var textBoxToo = this.Driver.SwitchTo().ActiveElement();

            Assert.IsTrue(textBox.Equals(textBoxToo));
        }

        #endregion
    }
}
