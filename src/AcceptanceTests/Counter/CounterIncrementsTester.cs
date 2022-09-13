using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Shouldly;

namespace AcceptanceTests.Counter
{
    public class CounterIncrementsTester
    {
        private TestDriver _testDriver;
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _testDriver = new TestDriver();
            _driver = _testDriver.GetDriver();
        }

        [TearDown]
        public void Teardown()
        {
            _testDriver.Dispose();
        }

        [Test]
        public void ShouldIncrementOnPress()
        {
            //arrange
            var hostAddress = "https://localhost:7174";//these environmental keys get refactored out
            _driver.Navigate().GoToUrl($"{hostAddress}/counter");
            var xPathForButton = By.CssSelector("button[ref='clickMeButton2']");

            //wait unitl screen comes up
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            IWebElement counterButton = wait.Until(driver => driver.FindElement(xPathForButton));
            _testDriver.TakeScreenshot(10, TestContext.CurrentContext.Test.FullName, "Arrange");

            var currentCountElement = _driver.FindElement(
                By.CssSelector($"#app > div > main > article > p"));
            currentCountElement.Text.ShouldContain("0");

            //act
            counterButton.Click();
            _testDriver.TakeScreenshot(20, TestContext.CurrentContext.Test.FullName, "Act");
            
            //assert
            currentCountElement.Text.ShouldContain("1");
            _testDriver.TakeScreenshot(30, TestContext.CurrentContext.Test.FullName, "Assert");
        }
    }
}