namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class GetElementAttributeTests : BaseForMainWindowTest
    {
        #region Fields

        private IWebElement textBox;

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void FindBaseElement()
        {
            this.textBox = this.MainWindow.FindElement(By.Id("TextBox1"));
        }

        [Test]
        public void GetNotSupportedAttribute()
        {
            var value = this.textBox.GetAttribute("InvalidAttributeName");

            Assert.AreEqual(null, value);
        }

        [Test]
        public void GetSupportedAttributeByFullPropertyName()
        {
            var value = this.textBox.GetAttribute("ClassNameProperty");

            Assert.AreEqual("TextBox", value);
        }

        [Test]
        public void GetSupportedAttributeByShortPropertyName()
        {
            var value = this.textBox.GetAttribute("ClassName");

            Assert.AreEqual("TextBox", value);
        }

        #endregion
    }
}
