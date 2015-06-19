namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    public class IsElementSelectedTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void IsSelectedCheckBox()
        {
            var checkbox = this.MainWindow.FindElement(By.Id("CheckBox1"));

            Assert.IsTrue(checkbox.Selected);
        }

        [Test]
        public void IsSelectedListItem()
        {
            var list = this.MainWindow.FindElement(By.Id("TextListBox"));
            var listItem = list.FindElement(By.Name("May"));

            listItem.Click();

            Assert.IsTrue(listItem.Selected);
        }

        [Test]
        public void IsSelectedTab()
        {
            var tab = this.MainWindow.FindElement(By.Name("TabItem1"));

            Assert.IsTrue(tab.Selected);
        }

        [Test]
        public void IsUnselectedCheckBox()
        {
            var checkbox = this.MainWindow.FindElement(By.Id("CheckBox1"));

            checkbox.Click();

            Assert.IsFalse(checkbox.Selected);
        }

        [Test]
        public void IsUnselectedListItem()
        {
            var list = this.MainWindow.FindElement(By.Id("TextListBox"));
            var listItem = list.FindElement(By.Name("May"));

            Assert.IsFalse(listItem.Selected);
        }

        [Test]
        public void IsUnselectedTab()
        {
            var tab = this.MainWindow.FindElement(By.Name("TabItem2"));

            Assert.IsFalse(tab.Selected);
        }

        #endregion
    }
}
