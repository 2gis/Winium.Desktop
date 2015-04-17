namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class ClickElementTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void ClickButtonWhichSetsText()
        {
            this.MainWindow.FindElement(By.Id("SetTextButton")).Click();

            Assert.AreEqual("CARAMBA", this.MainWindow.FindElement(By.Id("TextBox1")).Text);
        }

        #endregion
    }
}
