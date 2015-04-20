namespace WpfTestApplication.Tests.CommandTests
{
    #region using

    using System.Windows;
    using System.Windows.Forms;

    using NUnit.Framework;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    using Point = System.Drawing.Point;

    #endregion

    public class MouseMoveToTests : BaseForMainWindowTest
    {
        #region Constants

        private const int StartCrdValue = 10;

        #endregion

        #region Fields

        private Actions action;

        private IWebElement textBox;

        #endregion

        #region Public Methods and Operators

        [Test]
        public void MoveByOffset()
        {
            const int Offset = 50;

            this.action.MoveByOffset(Offset, Offset);
            this.action.Perform();
            var newPoint = Cursor.Position;

            Assert.That(StartCrdValue + Offset, Is.EqualTo(newPoint.X).Within(1));
            Assert.That(StartCrdValue + Offset, Is.EqualTo(newPoint.Y).Within(1));
        }

        [Test]
        public void MoveToElementWithOffset()
        {
            const int Offset = 50;
            var rect = Rect.Parse(this.textBox.GetAttribute("BoundingRectangle"));

            this.action.MoveToElement(this.textBox, Offset, Offset);
            this.action.Perform();
            var newPoint = Cursor.Position;

            Assert.That(rect.TopLeft.X + Offset, Is.EqualTo(newPoint.X).Within(1));
            Assert.That(rect.TopLeft.Y + Offset, Is.EqualTo(newPoint.Y).Within(1));
        }

        [Test]
        public void MoveToElementWithoutOffset()
        {
            var rect = Rect.Parse(this.textBox.GetAttribute("BoundingRectangle"));

            this.action.MoveToElement(this.textBox);
            this.action.Perform();
            var newPoint = Cursor.Position;

            Assert.That(rect.TopLeft.X + (rect.Width / 2), Is.EqualTo(newPoint.X).Within(1));
            Assert.That(rect.TopLeft.Y + (rect.Height / 2), Is.EqualTo(newPoint.Y).Within(1));
        }

        [SetUp]
        public void SetUpForFindElementAndCreateActionsClass()
        {
            this.textBox = this.MainWindow.FindElement(By.Id("TextBox1"));
            this.action = new Actions(this.Driver);
            Cursor.Position = new Point(StartCrdValue, StartCrdValue);
        }

        #endregion
    }
}
