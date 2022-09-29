namespace ProgrammingWithPalermo.ChurchBulletin.AcceptanceTests.Counter;

public class CounterIncrementsTester
{
    private IWebDriver _driver;
    private TestDriver _testDriver;

    [SetUp]
    public void Setup()
    {
        var configuration = TestHost.GetRequiredService<IConfiguration>();
        _testDriver = new TestDriver(configuration);
        _driver = _testDriver.GetDriver();
    }

    [TearDown]
    public void Teardown()
    {
        _testDriver.Dispose();
    }

    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(5, 5)]
    [TestCase(9, 9)]
    public void ShouldIncrementOnPress(int numberOfButtonPresses, int expectedFinalCount)
    {
        //arrange
        var hostAddress = "https://localhost:7174"; //these environmental keys get refactored out
        _driver.Navigate().GoToUrl($"{hostAddress}/counter");
        var xPathForButton = By.CssSelector("button[ref='clickMeButton2']");

        //wait unitl screen comes up
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        var counterButton = wait.Until(driver => driver.FindElement(xPathForButton));
        _testDriver.TakeScreenshot(10, TestContext.CurrentContext.Test.FullName, "Arrange");

        var currentCountElement = _driver.FindElement(
            By.CssSelector("#app > div > main > article > p"));
        currentCountElement.Text.ShouldContain("0");

        //act
        for (int i = 0; i < numberOfButtonPresses; i++)
        {
            counterButton.Click();
            _testDriver.TakeScreenshot(20+i, TestContext.CurrentContext.Test.FullName, "Act");
        }

        //assert
        currentCountElement.Text.ShouldContain(expectedFinalCount.ToString());
        _testDriver.TakeScreenshot(30, TestContext.CurrentContext.Test.FullName, "Assert");
    }
}