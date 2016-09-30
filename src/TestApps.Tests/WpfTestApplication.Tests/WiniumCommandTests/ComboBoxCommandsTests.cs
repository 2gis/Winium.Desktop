namespace WpfTestApplication.Tests.WiniumCommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [Ignore]
    public class ComboBoxCommandsTests : BaseTest<TestWebDriver>
    {
        #region Public Properties

        public IWebElement ComboBoxElement { get; set; }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void CollapseComboBox()
        {
            this.ComboBoxElement.Click();

            this.Driver.CollapseComboBox(this.ComboBoxElement);

            Assert.IsFalse(this.ComboBoxElement.FindElement(By.Name("Month")).Displayed);
        }

        [Test]
        public void ExpandComboBox()
        {
            this.Driver.ExpandComboBox(this.ComboBoxElement);

            Assert.IsTrue(this.ComboBoxElement.FindElement(By.Name("Month")).Displayed);
        }

        [Test]
        public void ExpectNotSurchElementExceptionIfNoItemsSelected()
        {
            Assert.Throws<NoSuchElementException>(() => this.Driver.FindComboBoxSelctedItem(this.ComboBoxElement));
        }

        [Test]
        public void FindSelectedItem()
        {
            this.ComboBoxElement.Click();

            var item = this.ComboBoxElement.FindElement(By.Name("Month"));

            item.Click();

            Assert.IsTrue(this.Driver.FindComboBoxSelctedItem(this.ComboBoxElement).Equals(item));
        }

        [Test]
        public void IsComboBoxExpanded()
        {
            this.ComboBoxElement.Click();

            Assert.IsTrue(this.Driver.IsComboBoxExpanded(this.ComboBoxElement));
        }

        [SetUp]
        public new void SetUp()
        {
            var mainWindow = this.Driver.FindElement(By.XPath("/*[@AutomationId='WpfTestApplicationMainWindow']"));
            this.ComboBoxElement = mainWindow.FindElement(By.Id("TextComboBox"));
        }

        #endregion
    }
}
