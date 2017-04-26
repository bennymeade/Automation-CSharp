using AAT.Helpers;
using AAT.Selectors;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace AAT.StepDefinitions
{
    [Binding]
    public class SharedSteps
    {
        private readonly IWebDriver _driver;

        private readonly SharedSelectors _sharedSelectors;

        public SharedSteps(IWebDriver driver)
        {
            _driver = driver;
            
            _sharedSelectors = new SharedSelectors();
            PageFactory.InitElements(_driver, _sharedSelectors);
        }



        [When(@"Select '(.*)' from filter dropdown menu")]
        public void WhenSelectFromFilterDropdownMenu(string menuItem)
        {
            TaskHelper.ExecuteTask(() =>
            {
                new WebDriverExtensions(_driver).WaitForPresence(_sharedSelectors.FilterDropdown);
                new WebDriverExtensions(_driver).SelectFromDropDownList(_sharedSelectors.FilterDropdown, menuItem);
            });
        }

        [Then(@"Validate page displayed '(.*)'")]
        public void ThenValidatePageDisplayed(string pageTitle)
        {
            TaskHelper.ExecuteTask(() => new WebDriverExtensions(_driver).WaitForPresence(_driver.FindElement(By.XPath(_sharedSelectors.PageTitle(pageTitle)))));
        }
    }
}
