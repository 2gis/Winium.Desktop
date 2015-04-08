namespace WpfTestApplication
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class CommandTests : BaseTest
    {
        #region Fields

        protected IWebElement TestWindow;

        #endregion

        #region Public Methods and Operators

        [Test]
        public void ClearElementTest()
        {
            var textBox = this.TestWindow.FindElement(By.Id("TextBox1"));
            textBox.Clear();

            Assert.AreEqual("", textBox.Text);
        }

        [Test]
        public void ClickElementTest()
        {
            this.TestWindow.FindElement(By.Id("SetTextButton")).Click();

            Assert.AreEqual("CARAMBA", this.TestWindow.FindElement(By.Id("TextBox1")).Text);
        }

        [Test]
        public void GetElementTextTest()
        {
            var textBox = this.TestWindow.FindElement(By.Id("TextBox1"));
            Assert.AreEqual("TextBox1", textBox.Text);
        }

        [Test]
        public void SendEmtyKeysToElementTest()
        {
            var textBox = this.TestWindow.FindElement(By.Id("TextBox1"));
            textBox.SendKeys("");

            Assert.AreEqual("", textBox.Text);
        }

        [Test]
        public void SendKeysToActiveElementTest()
        {
            var textbox = this.TestWindow.FindElement(By.Id("TextBox1"));
            textbox.Click();

            this.Driver.Keyboard.SendKeys("test");

            Assert.AreEqual("TextBox1test", textbox.Text);
        }

        [Test]
        public void SendKeysToElementTest()
        {
            const string NewText = "new test text";

            var textBox = this.TestWindow.FindElement(By.Id("TextBox1"));
            textBox.SendKeys(NewText);

            Assert.AreEqual(NewText, textBox.Text);
        }

        [SetUp]
        public void FindBaseElement()
        {
            this.TestWindow = this.Driver.FindElementById("WpfTestApplicationMainWindow");
        }

        #endregion
    }
}
