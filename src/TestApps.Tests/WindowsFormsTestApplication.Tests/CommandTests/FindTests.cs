namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class FindTests : BaseTest
    {
        #region Public Methods and Operators

        [Test]
        public void FindChildElementByClassName()
        {
            var parent = this.Driver.FindElementById("Form1");
            var child = parent.FindElement(By.ClassName("WindowsForms10.EDIT.app.0.2bf8098_r11_ad1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindChildElementById()
        {
            var parent = this.Driver.FindElementById("Form1");
            var child = parent.FindElement(By.Id("TextBox1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindChildElementByName()
        {
            var parent = this.Driver.FindElementById("Form1");
            var child = parent.FindElement(By.Name("TextBox1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindElementByClassName()
        {
            var element = this.Driver.FindElement(By.ClassName("WindowsForms10.EDIT.app.0.2bf8098_r11_ad1"));
            Assert.NotNull(element);
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
            var window = this.Driver.FindElementById("Form1");
            var comboBox = window.FindElement(By.Id("TextComboBox"));
            var elements = comboBox.FindElements(By.Id(string.Empty));

            Assert.AreEqual(6, elements.Count);
        }

        [Test]
        public void FindNoElements()
        {
            var window = this.Driver.FindElementById("Form1");
            var elements = window.FindElements(By.Id("UnexistId"));

            Assert.AreEqual(0, elements.Count);
        }

        #endregion
    }
}
