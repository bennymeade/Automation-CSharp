﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AAT.Selectors
{
    public class HeaderSelectors
    {
        [FindsBy(How = How.XPath, Using = "//a[contains(@class,'navbar-brand')]/span[contains(text(),'Home')]")]
        public IWebElement SiteLogo { get; set; }
    }
}
