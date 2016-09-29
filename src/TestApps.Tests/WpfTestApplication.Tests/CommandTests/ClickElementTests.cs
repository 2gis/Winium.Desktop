namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class ClickElementTests : BaseForMainWindowTest
    {
        #region Public Methods and Operators

        [Test]
        public void ClickButtonWhichSetsText()
        {
            this.MainWindow.FindElement(By.Id("SetTextButton")).Click();

            Assert.AreEqual("CARAMBA", this.MainWindow.FindElement(By.Id("TextBox1")).Text);
        }

        [Test]
        public void ClickByTwoElementsWithPressedControl()
        {
            var list = this.MainWindow.FindElement(By.Id("TextListBox"));

            var listItem1 = list.FindElement(By.Name("March"));
            var listItem2 = list.FindElement(By.Name("January"));
            var listItem3 = list.FindElement(By.Name("February"));

            this.Driver.ExecuteScript("input: ctrl_click", listItem1);
            this.Driver.ExecuteScript("input: ctrl_click", listItem2);

            Assert.IsTrue(listItem1.Selected);
            Assert.IsTrue(listItem2.Selected);
            Assert.IsFalse(listItem3.Selected);
        }

        [Test]
        public void ClickByElementBoundingRecatngleCenter()
        {
            var list = this.MainWindow.FindElement(By.Id("TextListBox"));

            var listItem1 = list.FindElement(By.Name("March"));

            this.Driver.ExecuteScript("input: brc_click", listItem1);

            Assert.IsTrue(listItem1.Selected);
        }

        #endregion
    }
}
