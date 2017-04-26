using System;
using AAT.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AAT.Helpers
{
    public class WebDriverExtensions
    {
        private IWebDriver _driver;

        public WebDriverExtensions(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public bool CaseInsensitiveContains(string text, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }

        public void JavaScriptClick(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        public void WaitForPresence(IWebElement element, int timeoutSeconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(Hooks.PollingIntervalMilliseconds)
            };
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(webDriver => element.Displayed);
        }

        public void WaitForDisappear(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                Timeout = TimeSpan.FromSeconds(Hooks.ExplicitWaitSeconds),
                PollingInterval = TimeSpan.FromMilliseconds(Hooks.PollingIntervalMilliseconds)
            };
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(webDriver => !element.Displayed);
        }

        public void SelectFromDropDownList(IWebElement element, String dropdowntext)
        {
            string listItemXpath = String.Empty;
            try
            {
                WaitForPresence(element);
                element.Click();
                listItemXpath = String.Format("//a[contains(text(),'{0}')]", dropdowntext);
                var listItem = element.FindElement(By.XPath(listItemXpath));
                WaitForPresence(listItem);
                listItem.Click();
            }
            catch (Exception e)
            {
                throw new ApplicationException(String.Format("{0}. Selector: {1}", e.Message, listItemXpath));
            }
        }

        public void SendKeys(string keys)
        {
            Actions action = new Actions(_driver);
            action.SendKeys(keys).Perform();
        }

        public void MoveToElement(IWebElement element)
        {
            WaitForPresence(element);
            try
            {
                Actions actions = new Actions(_driver);
                actions.MoveToElement(element);
                actions.Perform();
            }
            catch (Exception e)
            {
                throw new ApplicationException(String.Format("{0}. Selector: {1}", e.Message, element));
            }
        }

        public void ControlA(IWebElement element)
        {
            WaitForPresence(element);
            try
            {
                element.Click();
                element.SendKeys(Keys.Control + "a");
            }
            catch (Exception e)
            {
                throw new ApplicationException(String.Format("{0}. Selector: {1}", e.Message, element));
            }
        }
    }
}
