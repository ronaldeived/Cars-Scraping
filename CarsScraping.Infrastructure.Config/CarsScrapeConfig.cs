using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace CarsScrape.Infrastructure.Config
{
    public class CarsScrapeConfig : Interfaces.ICarsScrapeConfig
    {
        public CarsScrapeConfig()
        {
            IConfigurationBuilder ConfigBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("ConfigSettings.json");
            IConfigurationRoot AppConfig = ConfigBuilder.Build();
            new ConfigureFromConfigurationOptions<CarsScrapeConfig>(AppConfig.GetSection("CarsScrapeConfig")).Configure(this);
        }

        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string SubmitSelector { get ; set; }
        public string LoginFieldId { get; set; }
        public string PasswordId { get; set; }
        public string MakeCar { get; set; }
        public string ModelCar { get; set; }
        public string SearchButton { get; set; }
        public string Condition { get; set; }
        public string NameUserSelector { get; set; }
        public string CardVehicleSelector { get; set; }
        public string Url { get; set; }
    }
}
