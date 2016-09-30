namespace WpfTestApplication.Tests.WiniumCommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [Ignore]
    public class DataGridCommandsTests : BaseTest<TestWebDriver>
    {
        #region Public Properties

        public IWebElement DataGridElement { get; set; }

        #endregion

        #region Public Methods and Operators

        [Test]
        public void ExpectNotSurchElementExceptionIfCellUnavailableForSelection()
        {
            Assert.Throws<NoSuchElementException>(() => this.Driver.SelectDataGridCell(this.DataGridElement, 14, 1));
        }

        [Test]
        public void ExpectNotSurchElementExceptionIfDataGridCellNotExist()
        {
            Assert.Throws<NoSuchElementException>(() => this.Driver.FindDataGridCell(this.DataGridElement, 99, 9));
        }

        [Test]
        public void ExpectNotSurchElementExceptionIfDataGridCellUnavailable()
        {
            Assert.Throws<NoSuchElementException>(() => this.Driver.FindDataGridCell(this.DataGridElement, 14, 1));
        }

        [Test]
        public void FindDataGridCell()
        {
            var dataGridCell = this.Driver.FindDataGridCell(this.DataGridElement, 0, 1);

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

            Assert.AreEqual(15, rowCount);
        }

        [Test]
        public void ScrollToDataGridCell()
        {
            this.Driver.ScrollToDataGridCell(this.DataGridElement, 14, 1);

            var dataGridCell = this.Driver.FindDataGridCell(this.DataGridElement, 14, 1);

            Assert.IsTrue(dataGridCell.Displayed);
        }

        [Test]
        public void SelectDataGridCell()
        {
            this.Driver.SelectDataGridCell(this.DataGridElement, 1, 1);
            var dataGridCell = this.Driver.FindDataGridCell(this.DataGridElement, 1, 1);

            var dataGridCellToo = this.Driver.SwitchTo().ActiveElement();

            Assert.IsTrue(dataGridCell.Equals(dataGridCellToo));
        }

        [SetUp]
        public new void SetUp()
        {
            var mainWindow = this.Driver.FindElement(By.XPath("/*[@AutomationId='WpfTestApplicationMainWindow']"));
            var tab = mainWindow.FindElement(By.Name("TabItem4"));
            tab.Click();

            this.DataGridElement = tab.FindElement(By.Id("DataGrid"));
        }

        #endregion
    }
}
