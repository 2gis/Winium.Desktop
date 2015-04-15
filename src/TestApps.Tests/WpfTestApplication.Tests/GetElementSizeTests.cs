namespace WpfTestApplication.Tests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class GetElementSizeTests : BaseTest
    {
        #region Public Methods and Operators

        [TestCase("TextBox1", 200, 23)]
        [TestCase("TextListBox", 200, 100)]
        public void GetSizeOfElement(string elementId, int width, int height)
        {
            var element = this.Driver.FindElementById("WpfTestApplicationMainWindow").FindElement(By.Id(elementId));

            var size = element.Size;

            Assert.AreEqual(width, size.Width);
            Assert.AreEqual(height, size.Height);
        }

        #endregion
    }
}
