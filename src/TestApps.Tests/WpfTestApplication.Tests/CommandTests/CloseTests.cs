namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using System.Diagnostics;

    using NUnit.Framework;

    using OpenQA.Selenium;

    #endregion

    [TestFixture]
    public class CloseTests
    {
        #region Fields

        private Process appProcess;

        private BaseForMainWindowTest baseForMainWindowTest;

        #endregion

        #region Public Methods and Operators

        [Test]
        public void CloseApplication()
        {
            this.baseForMainWindowTest.TearDown();

            Assert.IsTrue(this.appProcess.HasExited);
        }

        [Test]
        public void CloseApplicationWithOpenedDialogWindow()
        {
            this.baseForMainWindowTest.FindMainWindow();
            var tabItem3 = this.baseForMainWindowTest.MainWindow.FindElement(By.Id("TabItem3"));
            tabItem3.Click();
            tabItem3.FindElement(By.Id("OpenFileDialogButton")).Click();

            this.baseForMainWindowTest.TearDown();

            Assert.IsTrue(this.appProcess.HasExited);
        }

        [SetUp]
        public void SetUp()
        {
            this.baseForMainWindowTest = new BaseForMainWindowTest();
            this.baseForMainWindowTest.SetUp();

            this.appProcess = Process.GetProcessesByName("WpfTestApplication")[0];
        }

        #endregion
    }
}
