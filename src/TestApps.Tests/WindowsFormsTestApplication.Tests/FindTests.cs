namespace WindowsFormsTestApplication.Tests
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
        public void FindChildElementByClassNameTest()
        {
            var parent = this.Driver.FindElementById("Form1");
            var child = parent.FindElement(By.ClassName("WindowsForms10.EDIT.app.0.2bf8098_r11_ad1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindChildElementByIdTest()
        {
            var parent = this.Driver.FindElementById("Form1");
            var child = parent.FindElement(By.Id("TextBox1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindChildElementByNameTest()
        {
            var parent = this.Driver.FindElementById("Form1");
            var child = parent.FindElement(By.Name("TextBox1"));
            Assert.NotNull(child);
        }

        [Test]
        public void FindElementByClassNameTest()
        {
            var element = this.Driver.FindElement(By.ClassName("WindowsForms10.EDIT.app.0.2bf8098_r11_ad1"));
            Assert.NotNull(element);
        }

        [Test]
        public void FindElementByIdTest()
        {
            var element = this.Driver.FindElement(By.Id("TextBox1"));
            Assert.NotNull(element);
        }

        [Test]
        public void FindElementByNameTest()
        {
            var element = this.Driver.FindElement(By.Name("TextBox1"));
            Assert.NotNull(element);
        }

        [Test]
        public void FindElementsTest()
        {
            var window = this.Driver.FindElementById("Form1");
            var comboBox = window.FindElement(By.Id("TextComboBox"));
            var elements = comboBox.FindElements(By.Id(string.Empty));

            Assert.AreEqual(6, elements.Count);
        }

        [Test]
        public void FindNoElementsTest()
        {
            var window = this.Driver.FindElementById("Form1");
            var elements = window.FindElements(By.Id("UnexistId"));

            Assert.AreEqual(0, elements.Count);
        }

        #endregion
    }
}
