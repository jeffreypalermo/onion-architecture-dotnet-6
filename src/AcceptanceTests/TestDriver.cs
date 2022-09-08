using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProgrammingWithPalermo.ChurchBulletin.IntegrationTests;

namespace AcceptanceTests;

public class TestDriver : IDisposable
{
    private const string ChromeWebDriver = "ChromeWebDriver";
    private ChromeDriver _driver;

    public void Dispose()
    {
        if (_driver == null) return;
        _driver.Close();
        _driver.Quit();
        _driver.Dispose();
        _driver = null;
    }

    public IWebDriver GetDriver()
    {
        if (_driver != null) return _driver;
        var _configuration = TestHost.Instance.Services.GetRequiredService<IConfiguration>();
        // Path to ChromeDriver.exe provided by "ChromeWebDriver" ENV var in Azure Pipelines Hosted Agent
        // https://github.com/Microsoft/azure-pipelines-image-generation/blob/master/images/win/Vs2019-Server2019-Readme.md#chrome-driver
        var _driverPath = Environment.GetEnvironmentVariable(ChromeWebDriver)
                          ?? _configuration.GetValue<string>("ChromeWebDriver")
                          ?? ".";
        TestContext.WriteLine($"ChromeWebDriver being used is {_driverPath}");
        var chromeOptions = new ChromeOptions();

        //chromeOptions.AddArgument("headless");
        _driver = new ChromeDriver(_driverPath, chromeOptions);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        return _driver;
    }

    public void TakeScreenshot(ushort stepNumber, string testName, string stepName)
    {
        var chromeDriver = (ChromeDriver)GetDriver();
        var filename = $"{testName}-{stepNumber:000}-{stepName}.png";
        chromeDriver.GetScreenshot().SaveAsFile(filename);
        TestContext.AddTestAttachment(Path.GetFullPath(filename));
    }
}