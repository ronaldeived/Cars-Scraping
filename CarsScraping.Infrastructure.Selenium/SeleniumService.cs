using CarsScrape.Infrastructure.Selenium.Interfaces;
using CarsScraping.Infrastructure.Selenium.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CarsScrape.Infrastructure.Selenium
{
    public class SeleniumService : ISeleniumServiceContract
    {

        private IWebDriver _Browser;
        private int _WaitElementTimeout;

        public SeleniumService(ISeleniumConfigContract pSeleniumConfig)
        {
            //This constructor is for choosing which browser will be used to scrape the screen.
            //The browser is chosen in the SeleniumSettings.json file
            if (pSeleniumConfig.Browser == "Chrome")
            {
                ChromeOptions optChrome = new ChromeOptions();
                if (pSeleniumConfig.IsHeadless)
                    optChrome.AddArgument("--headless");
                _Browser = new ChromeDriver(pSeleniumConfig.DriverPath, optChrome);
            }
            else if (pSeleniumConfig.Browser == "Firefox")
            {
                FirefoxOptions optFX = new FirefoxOptions();
                if (pSeleniumConfig.IsHeadless)
                    optFX.AddArgument("--headless");
                _Browser = new FirefoxDriver(pSeleniumConfig.DriverPath, optFX);
            }
            else if (pSeleniumConfig.Browser == "NewEdge")
            {
                EdgeOptions edgeOptions = new EdgeOptions();
                edgeOptions.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
                if (pSeleniumConfig.IsHeadless)
                    edgeOptions.AddArgument("--headless");

                string msedgedriverDir = pSeleniumConfig.DriverPath;
                string msedgedriverExe = @"msedgedriver.exe";
                EdgeDriverService service = EdgeDriverService.CreateDefaultService(msedgedriverDir, msedgedriverExe);
                service.EnableVerboseLogging = true;
                _Browser = new EdgeDriver(service, edgeOptions);
            }

            _Browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pSeleniumConfig.NavigationTimeout);
            _WaitElementTimeout = pSeleniumConfig.WaitElementTimeout;
        }

        public void SetElementValueBy(string pBy, string pSelector, string pValue)
        {
            WebDriverWait TimeWait = new WebDriverWait(_Browser, TimeSpan.FromSeconds(_WaitElementTimeout));
            TimeWait.Until(e => e.FindElement(GetBy(pBy, pSelector))).SendKeys(pValue);
        }

        public void Navigate(string pUrl)
        {
            _Browser.Navigate().GoToUrl(pUrl);
        }

        public string GetNameUser(string pBy, string pSelector)
        {
            string name = string.Empty;
            string concatItem = string.Empty;
            string formattedName = string.Empty;
            WebDriverWait wait = new WebDriverWait(_Browser, TimeSpan.FromSeconds(10));
            wait.Until(a => name = a.FindElement(GetBy(pBy, pSelector)).Text);
            
            foreach (var value in name)
            {
                concatItem += value;
                if (concatItem.Contains("Hi, ")) formattedName += value;
            }

            return formattedName;
        }

        public List<Make_ModelCarSelenium> GetSelectOptions(string pBy, string pSelector, string? make, string? pSelectorMake)
        {
            try
            {
                WebDriverWait timeWait;
                SelectElement oSelect;

                //This condition was created specifically for choosing the car model, because before choosing the model, the car make needs to be selected
                if (make is not null && pSelectorMake is not null)
                {
                    timeWait = new WebDriverWait(_Browser, TimeSpan.FromSeconds(_WaitElementTimeout));
                    oSelect = new SelectElement(timeWait.Until(e => e.FindElement(GetBy(pBy, pSelectorMake))));
                    oSelect.SelectByText(make);
                    oSelect.SelectedOption.Click();
                }

                //Here select the model car.
                timeWait = new WebDriverWait(_Browser, TimeSpan.FromSeconds(_WaitElementTimeout));
                oSelect = new SelectElement(timeWait.Until(e => e.FindElement(GetBy(pBy, pSelector))));
                IList<IWebElement> ElementList = oSelect.Options;

                return (List<Make_ModelCarSelenium>) MountSelect(ElementList);
            }
            catch (WebDriverTimeoutException)
            {
                throw;
            }
        }

        public void ClickElementBy(string pBy, string pSelector)
        {
            try
            {
                WebDriverWait TimeWait = new WebDriverWait(_Browser, TimeSpan.FromSeconds(_WaitElementTimeout));
                TimeWait.Until(e => e.FindElement(GetBy(pBy, pSelector))).Click();
            }
            catch (WebDriverTimeoutException)
            { }
        }

        public void ClickInSelectElementBy(string pBy, string pSelector, string pValue, bool useTextValue)
        {
            try
            {
                WebDriverWait TimeWait = new WebDriverWait(_Browser, TimeSpan.FromSeconds(_WaitElementTimeout));
                SelectElement oSelect = new SelectElement(TimeWait.Until(e => e.FindElement(GetBy(pBy, pSelector))));

                //This condition is to check how selenium will search for the select option.
                if (useTextValue) oSelect.SelectByText(pValue);
                else oSelect.SelectByValue(pValue);

                oSelect.SelectedOption.Click();
            }
            catch (WebDriverTimeoutException)
            { }
        }

        public IList<IWebElement> GetElementsBy(string pBy, string pSelector)
        {
            return _Browser.FindElements(GetBy(pBy, pSelector));
        }

        private By GetBy(string pBy, string pSelector)
        {
            By retBy;
            switch (pBy)
            {
                case "class":
                    retBy = By.ClassName(pSelector);
                    break;
                case "id":
                    retBy = By.Id(pSelector);
                    break;
                case "xpath":
                    retBy = By.XPath(pSelector);
                    break;
                default:
                    retBy = By.ClassName(pSelector);
                    break;
            }

            return retBy;
        }

        private IEnumerable<Make_ModelCarSelenium> MountSelect(IList<IWebElement> listElements)
        {
            List<Make_ModelCarSelenium> element = new List<Make_ModelCarSelenium>();

            foreach (IWebElement item in listElements) element.Add(OrganizeElements(item));

            return element;
        }

        private Make_ModelCarSelenium OrganizeElements(IWebElement item)
        { 
            return new Make_ModelCarSelenium()
            {
                Id = item.ToString(),
                Text = item.Text,
            };
        }
    }
}
