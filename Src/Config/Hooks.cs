using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace FordTA2_AAT.Config
{
    public class CustomRemoteWebDriver : RemoteWebDriver
    {
        public CustomRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities)
        {
        }

        public string getSessionID()
        {
            return base.SessionId.ToString();
        }
    }
    
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public const int ExplicitWaitSeconds = 10;
        public const int PollingIntervalMilliseconds = 100;

        // Run Locally - IE Driver
        //[BeforeScenario]
        //public void Initialise()
        //{
        //    IWebDriver _driver;

        //    _driver = new InternetExplorerDriver();
        //    _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
        //    _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("BaseUrl"));
        //    _driver.Manage().Window.Maximize();
        //}

        // Run Locally - Chrome Driver
        //[BeforeScenario]
        //public void Initialise()
        //{
        //    IWebDriver _driver;
        //    _driver = new ChromeDriver();
        //    _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
        //    _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("BaseUrl"));
        //    _driver.Manage().Window.Maximize();
        //}


        CustomRemoteWebDriver _driver;
        DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
        readonly string isDesktopBrowser = ConfigurationManager.AppSettings.Get("IsDesktopBrowser");

        // Run on BrowserStack
        [BeforeScenario]
        public void Initialise()
        {
            BrowserStackCapabilities();

            _driver = new CustomRemoteWebDriver(new Uri(ConfigurationManager.AppSettings.Get("BrowserStackUrl")), capability);
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings.Get("BaseUrl"));

            if (isDesktopBrowser == "true")
                _driver.Manage().Window.Maximize();

            GetSessionId();
        }

        private void BrowserStackCapabilities()
        {
            capability.SetCapability("browserstack.user", (ConfigurationManager.AppSettings.Get("BrowserStackUser")));
            capability.SetCapability("browserstack.key", (ConfigurationManager.AppSettings.Get("BrowserStackKey")));
            capability.SetCapability("browserstack.debug", (ConfigurationManager.AppSettings.Get("BrowserStackDebug")));


            if (isDesktopBrowser == "true")
            {
                // Desktop Browser
                capability.SetCapability("browser", (ConfigurationManager.AppSettings.Get("BrowserType")));
                capability.SetCapability("browser_version", (ConfigurationManager.AppSettings.Get("BrowserVersion")));
                capability.SetCapability("os", (ConfigurationManager.AppSettings.Get("OsType")));
                capability.SetCapability("os_version", (ConfigurationManager.AppSettings.Get("OsVersion")));
                capability.SetCapability("resolution", (ConfigurationManager.AppSettings.Get("OsResolution")));
            }
            else if (isDesktopBrowser == "false")
            {
                // Mobile Browser
                capability.SetCapability("browserName", (ConfigurationManager.AppSettings.Get("BrowserName")));
                capability.SetCapability("platform", (ConfigurationManager.AppSettings.Get("Platform")));
                capability.SetCapability("device", (ConfigurationManager.AppSettings.Get("Device")));
            }
            else
            {
                Console.WriteLine("*** Browser type not set in App Config");
            }
        }

        private void GetSessionId()
        {
            string sessionId = _driver.getSessionID();
            Console.WriteLine("BROWSERSTACK SESSION ID: " + sessionId);
        }

        [AfterScenario]
        public void CleanUp()
        {
            if (_driver != null)
                _driver.Quit();
        }
    }
}
