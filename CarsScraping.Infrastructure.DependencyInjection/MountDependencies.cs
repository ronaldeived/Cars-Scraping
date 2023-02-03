using CarsScrape.Infrastructure.Config;
using CarsScrape.Infrastructure.Config.Interfaces;
using CarsScrape.Infrastructure.Selenium;
using CarsScrape.Infrastructure.Selenium.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarsScrape.Infrastructure.DependencyInjection
{
    public class MountDependencies
    {
        private ServiceProvider _ServiceList;
        public MountDependencies()
        {
            _ServiceList = new ServiceCollection()
                .AddTransient<ICarsScrapeConfig, CarsScrapeConfig>()
                .AddTransient<ICarsScrapeService, CarsScrapeService>()
                .AddTransient<ISeleniumConfigContract, SeleniumConfig>()
                .AddTransient<ISeleniumServiceContract, SeleniumService>()
                .BuildServiceProvider();
        }

        public ICarsScrapeService CarsScraping() => _ServiceList.GetRequiredService<ICarsScrapeService>();
    }
}
