using CarsScraping.Infrastructure.Selenium.Model;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace CarsScrape.Infrastructure.Selenium.Interfaces
{
    public interface ISeleniumServiceContract
    {
        void Navigate(string pUrl);
        IList<IWebElement> GetElementsBy(string pBy, string pSelector);
        void SetElementValueBy(string pBy, string pSelector, string pValue);
        void ClickElementBy(string pBy, string pSelector);
        void ClickInSelectElementBy(string pBy, string pSelector, string pValue, bool useTextValue);
        List<Make_ModelCarSelenium> GetSelectOptions(string pBy, string pSelector, string? make, string? pselectorMake);
        string GetNameUser(string pBy, string pSelector);
    }
}
