using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AAT.Selectors
{
    public class SharedSelectors
    {
        public string PageTitle(string title)
        {
            return string.Format("//h4[contains(text(),'{0}')]", title);
        }

        [FindsBy(How = How.Id, Using = "filter-dropdown")]
        public IWebElement FilterDropdown { get; set; }

    }
}
