namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using System.Diagnostics;

    using NUnit.Framework;

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

        [Test, Ignore("Application not have needed elements")]
        public void CloseApplicationWithOpenedDialogWindow()
        {
            // TODO: Extend WindowsFormsTestApplication
        }

        [SetUp]
        public void SetUp()
        {
            this.baseForMainWindowTest = new BaseForMainWindowTest();
            this.baseForMainWindowTest.SetUp();

            this.appProcess = Process.GetProcessesByName("WindowsFormsTestApplication")[0];
        }

        #endregion
    }
}
