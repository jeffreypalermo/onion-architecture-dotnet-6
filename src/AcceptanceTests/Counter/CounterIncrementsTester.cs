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
            var hostAddress = "https://localhost:7174";
            _driver.Navigate().GoToUrl($"{hostAddress}/counter");
            var xPathForButton = By.XPath($@"//*[@id=""app""]/div/main/article/button");

            //wait unitl screen comes up
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            IWebElement counterButton = wait.Until(driver => driver.FindElement(xPathForButton));
            _testDriver.TakeScreenshot(10, TestContext.CurrentContext.Test.FullName, "Arrange");

            var currentCountElement = _driver.FindElement(By.CssSelector($"#app > div > main > article > p"));
            currentCountElement.Text.ShouldContain("0");

            counterButton.Click();
            _testDriver.TakeScreenshot(20, TestContext.CurrentContext.Test.FullName, "Act");

            currentCountElement.Text.ShouldContain("1");
            _testDriver.TakeScreenshot(30, TestContext.CurrentContext.Test.FullName, "Assert");
        }
    }
}