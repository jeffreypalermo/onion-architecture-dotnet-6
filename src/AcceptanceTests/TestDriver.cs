namespace ProgrammingWithPalermo.ChurchBulletin.AcceptanceTests;

public class TestDriver : IDisposable
{
    private const string ChromeWebDriver = "ChromeWebDriver";
    private readonly ChromeDriver _driver;

    public TestDriver(IConfiguration configuration)
    {
        // Path to ChromeDriver.exe provided by "ChromeWebDriver" ENV var in Azure Pipelines Hosted Agent
        // https://github.com/Microsoft/azure-pipelines-image-generation/blob/master/images/win/Vs2019-Server2019-Readme.md#chrome-driver
        var _driverPath = Environment.GetEnvironmentVariable(ChromeWebDriver)
                          ?? configuration.GetValue<string>("ChromeWebDriver")
                          ?? ".";
        TestContext.WriteLine($"ChromeWebDriver being used is {_driverPath}");
        var chromeOptions = new ChromeOptions();

        //chromeOptions.AddArgument("headless");
        _driver = new ChromeDriver(_driverPath, chromeOptions);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    public void Dispose()
    {
        _driver.Close();
        _driver.Quit();
        _driver.Dispose();
        GC.SuppressFinalize(this);
    }

    public IWebDriver GetDriver()
    {
        return _driver;
    }

    public void TakeScreenshot(int stepNumber, string testName, string stepName)
    {
        var chromeDriver = (ChromeDriver) GetDriver();
        var filename = $"{testName}-{stepNumber:000}-{stepName}.png";
        chromeDriver.GetScreenshot().SaveAsFile(filename);
        TestContext.AddTestAttachment(Path.GetFullPath(filename));
    }
}