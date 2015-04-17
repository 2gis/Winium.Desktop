namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class GetElementTextTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void GetTextBoxText()
        {
            var textBox = this.MainWindow.FindElement(By.Id("TextBox1"));
            Assert.AreEqual("TextBox1", textBox.Text);
        }

        #endregion
    }
}
