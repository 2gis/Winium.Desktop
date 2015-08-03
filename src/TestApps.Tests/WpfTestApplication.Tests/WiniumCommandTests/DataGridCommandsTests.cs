namespace WpfTestApplication.Tests.WiniumCommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class DataGridCommandsTests : BaseTest<TestWebDriver>
    {
        #region Public Properties

        public IWebElement DataGridElement { get; set; }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void GetDataGridCell()
        {
            var dataGridCell = this.Driver.GetDataGridCell(this.DataGridElement, 0, 1);

            Assert.AreEqual("one", dataGridCell.Text);
        }

        [Test]
        public void GetDataGridColumnCount()
        {
            var columnCount = this.Driver.GetDataGridColumnCount(this.DataGridElement);

            Assert.AreEqual(2, columnCount);
        }

        [Test]
        public void GetDataGridRowCount()
        {
            var rowCount = this.Driver.GetDataGridRowCount(this.DataGridElement);

            Assert.AreEqual(5, rowCount);
        }

        [Test]
        public void ScrollToDataGridCell()
        {
            this.Driver.ScrollToDataGridCell(this.DataGridElement, 0, 4);

            Assert.IsTrue(this.DataGridElement.Displayed);
        }

        [SetUp]
        public new void SetUp()
        {
            var mainWindow = this.Driver.FindElementById("WpfTestApplicationMainWindow");
            var tab = mainWindow.FindElement(By.Name("TabItem4"));
            tab.Click();

            this.DataGridElement = tab.FindElement(By.Id("DataGrid"));
        }

        #endregion
    }
}
