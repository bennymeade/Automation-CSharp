using FordTA2_AAT.Helpers;
using FordTA2_AAT.Selectors;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace FordTA2_AAT.StepDefinitions
{
    [Binding]
    public class HomepageSteps
    {
        private readonly IWebDriver _driver;
        
        
        private readonly HeaderSelectors _headerSelectors;
        
        public HomepageSteps(IWebDriver driver)
        {
            _driver = driver;
            
            _headerSelectors = new HeaderSelectors();
            PageFactory.InitElements(_driver, _headerSelectors);
        }

        

        [Given(@"Homepage has launched")]
        public void GivenHomepageHasLaunched()
        {
            TaskHelper.ExecuteTask(() => new WebDriverExtensions(_driver).WaitForPresence(_headerSelectors.SiteLogo));
        }
    }
}
