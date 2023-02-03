using CarsScrape.Model;
using CarsScraping.Infrastructure.Selenium.Model;
using System.Collections.Generic;

namespace CarsScrape.Infrastructure.Config.Interfaces
{
    public interface ICarsScrapeService
    {
        IEnumerable<Car> GetCars(string make, string model);
        void SignIn();
        List<Make_ModelCarSelenium> GetMakeCar();
        List<Make_ModelCarSelenium> GetModelCar(string make);
        string GetNameUser();
        void GoHome();
    }
}
