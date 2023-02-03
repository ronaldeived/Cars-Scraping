using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace CarsScrape.Infrastructure.Selenium
{
    public class SeleniumConfig : Interfaces.ISeleniumConfigContract
    {
        public string DriverPath { get; set; }
        public int NavigationTimeout { get; set; }
        public bool IsHeadless { get; set; }
        public int WaitElementTimeout { get; set; }
        public string Browser { get; set; }
        public SeleniumConfig()
        {
            IConfigurationBuilder ConfigBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("SeleniumSettings.json");
            IConfigurationRoot AppConfig = ConfigBuilder.Build();
            new ConfigureFromConfigurationOptions<SeleniumConfig>(AppConfig.GetSection("SeleniumConfig")).Configure(this);
        }
    }
}
