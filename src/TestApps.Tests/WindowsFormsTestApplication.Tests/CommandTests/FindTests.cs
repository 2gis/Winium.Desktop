namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class FindTests : BaseTest
    {
        #region Public Properties

        public IWebElement MainWindow
        {
            get
            {
                var mainWindowStrategy = By.XPath("/*[@AutomationId='Form1']");
                return this.Driver.FindElement(mainWindowStrategy);
            }
        }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void FindChildElementById()
        {
            var child = this.MainWindow.FindElement(By.Id("TextBox1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindChildElementByName()
        {
            var child = this.MainWindow.FindElement(By.Name("TextBox1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindElementById()
        {
            var element = this.Driver.FindElement(By.Id("TextBox1"));
            Assert.NotNull(element);
        }

        [Test]
        public void FindElementByName()
        {
            var element = this.Driver.FindElement(By.Name("TextBox1"));
            Assert.NotNull(element);
        }

        [Test]
        public void FindElements()
        {
            var comboBox = this.MainWindow.FindElement(By.Id("TextComboBox"));
            var elements = comboBox.FindElements(By.Id(string.Empty));
            Assert.AreEqual(6, elements.Count);
        }

        [Test]
        public void FindNoElements()
        {
            var elements = this.MainWindow.FindElements(By.Id("UnexistId"));
            Assert.AreEqual(0, elements.Count);
        }

        #endregion
    }
}
