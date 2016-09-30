namespace WpfTestApplication.Tests.WiniumCommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [Ignore]
    public class MenuCommandsTests : BaseTest<TestWebDriver>
    {
        #region Public Properties

        public IWebElement MenuElement { get; set; }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void ExpectNotSurchElementExceptionIfGetNotExistMenuItem()
        {
            Assert.Throws<NoSuchElementException>(
                () => this.Driver.FindMenuItem(this.MenuElement, "Level1$NotExistItem"));
        }

        [Test]
        public void ExpectNotSurchElementExceptionIfSelectNotExistMenuItem()
        {
            Assert.Throws<NoSuchElementException>(
                () => this.Driver.SelectMenuItem(this.MenuElement, "Level1$NotExistItem"));
        }

        [Test]
        public void FindMenuItem()
        {
            var menuItem = this.Driver.FindMenuItem(this.MenuElement, "Level1$MultiLevel2$Level3");
            Assert.NotNull(menuItem);
        }

        [SetUp]
        public new void SetUp()
        {
            var mainWindow = this.Driver.FindElement(By.XPath("/*[@AutomationId='WpfTestApplicationMainWindow']"));
            this.MenuElement = mainWindow.FindElement(By.Id("SimpleMenu"));
        }

        #endregion
    }
}
