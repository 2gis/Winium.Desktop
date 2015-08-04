namespace WpfTestApplication.Tests.WiniumCommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class ComboBoxCommandsTests : BaseTest<TestWebDriver>
    {
        #region Public Properties

        public IWebElement ComboBoxElement { get; set; }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void IsComboBoxExpanded()
        {
            this.ComboBoxElement.Click();

            Assert.IsTrue(this.Driver.IsComboBoxExpanded(this.ComboBoxElement));
        }

        [Test]
        public void ExpandComboBox()
        {
            this.Driver.ExpandComboBox(this.ComboBoxElement);

            Assert.IsTrue(this.ComboBoxElement.FindElement(By.Name("Month")).Displayed);
        }

        [Test]
        public void CollapseComboBox()
        {
            this.ComboBoxElement.Click();

            this.Driver.CoollapseComboBox(this.ComboBoxElement);

            Assert.IsFalse(this.ComboBoxElement.FindElement(By.Name("Month")).Displayed);
        }

        [Test]
        public void GetSelectedItem()
        {
            this.ComboBoxElement.Click();

            var item = this.ComboBoxElement.FindElement(By.Name("Month"));
            
            item.Click();

            Assert.IsTrue(this.Driver.GetComboBoxSelctedItem(this.ComboBoxElement).Equals(item));
        }

        [SetUp]
        public new void SetUp()
        {
            var mainWindow = this.Driver.FindElementById("WpfTestApplicationMainWindow");
            this.ComboBoxElement = mainWindow.FindElement(By.Id("TextComboBox"));
        }

        #endregion
    }
}
