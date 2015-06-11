namespace WindowsFormsTestApplication.Tests.CommandTests
{
    #region using

    using System;
    using System.IO;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    #endregion

    public class KeyboardSimulatorSelectTests
    {
        #region Private Properties

        private RemoteWebDriver Driver { get; set; }
        private IWebElement MainWindow { get; set; }

        #endregion

        #region Methods and Operators

        [Test]
        public void InputSimulatorTest()
        {
            this.InitDriverWithKeyboard("BasedOnInputSimulatorLib");
            var textbox = this.MainWindow.FindElement(By.Id("TextBox1"));
            textbox.Click();

            this.Driver.Keyboard.SendKeys("test");

            Assert.AreEqual("TextBox1test", textbox.Text);
        }

        [Test]
        public void SendKeysTest()
        {
            this.InitDriverWithKeyboard("BasedOnWindowsFormsSendKeysClass");
            var textbox = this.MainWindow.FindElement(By.Id("TextBox1"));
            textbox.Click();

            this.Driver.Keyboard.SendKeys("test");

            Assert.AreEqual("TextBox1test", textbox.Text);
        }

        [TearDown]
        public void TearDown()
        {
            this.Driver.Close();
        }

        private void InitDriverWithKeyboard(string keyboardSimulator)
        {
            var dc = new DesiredCapabilities();
            dc.SetCapability("app", Path.Combine(Environment.CurrentDirectory, "WindowsFormsTestApplication.exe"));
            dc.SetCapability("keyboardSimulator", keyboardSimulator);
            this.Driver = new RemoteWebDriver(new Uri("http://localhost:9999"), dc);
            this.MainWindow = this.Driver.FindElementById("Form1");
        }

        #endregion
    }
}
