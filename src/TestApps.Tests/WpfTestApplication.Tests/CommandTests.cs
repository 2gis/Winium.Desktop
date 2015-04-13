namespace WpfTestApplication.Tests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class CommandTests : BaseTest
    {
        #region Fields

        private IWebElement testWindow;

        #endregion

        #region Public Methods and Operators

        [Test]
        public void ClearElementTest()
        {
            var textBox = this.testWindow.FindElement(By.Id("TextBox1"));
            textBox.Clear();

            Assert.AreEqual(string.Empty, textBox.Text);
        }

        [Test]
        public void ClickElementTest()
        {
            this.testWindow.FindElement(By.Id("SetTextButton")).Click();

            Assert.AreEqual("CARAMBA", this.testWindow.FindElement(By.Id("TextBox1")).Text);
        }

        [SetUp]
        public void FindBaseElement()
        {
            this.testWindow = this.Driver.FindElementById("WpfTestApplicationMainWindow");
        }

        [Test]
        public void GetElementAttributeTest()
        {
            var element = this.testWindow.FindElement(By.Id("TextBox1"));
            Assert.AreEqual("TextBox", element.GetAttribute("ClassName"));
        }

        [Test]
        public void GetElementTextTest()
        {
            var textBox = this.testWindow.FindElement(By.Id("TextBox1"));
            Assert.AreEqual("TextBox1", textBox.Text);
        }

        [Test]
        public void SendEmptyKeysToElementTest()
        {
            var textBox = this.testWindow.FindElement(By.Id("TextBox1"));
            textBox.SendKeys(string.Empty);

            Assert.AreEqual(string.Empty, textBox.Text);
        }

        [Test]
        public void SendKeysToActiveElementTest()
        {
            var textbox = this.testWindow.FindElement(By.Id("TextBox1"));
            textbox.Click();

            this.Driver.Keyboard.SendKeys("test");

            Assert.AreEqual("TextBox1test", textbox.Text);
        }

        [Test]
        public void SendKeysToElementTest()
        {
            const string NewText = "new test text";

            var textBox = this.testWindow.FindElement(By.Id("TextBox1"));
            textBox.SendKeys(NewText);

            Assert.AreEqual(NewText, textBox.Text);
        }

        #endregion
    }
}
