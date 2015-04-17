namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class SendKeysToActiveElementTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void SendKeysToActiveElement()
        {
            var textbox = this.MainWindow.FindElement(By.Id("TextBox1"));
            textbox.Click();

            this.Driver.Keyboard.SendKeys("test");

            Assert.AreEqual("TextBox1test", textbox.Text);
        }

        #endregion
    }
}
