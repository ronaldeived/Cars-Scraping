using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsScrape.Infrastructure.Selenium.Interfaces
{
    public interface ISeleniumConfigContract
    {
        string DriverPath { get; set; }
        bool IsHeadless { get; set; }
        int NavigationTimeout { get; set; }
        int WaitElementTimeout { get; set; }
        string Browser { get; set; }
    }
}
