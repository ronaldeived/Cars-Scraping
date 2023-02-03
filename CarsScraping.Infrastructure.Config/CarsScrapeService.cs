using CarsScrape.Infrastructure.Config.Interfaces;
using CarsScrape.Infrastructure.Selenium.Interfaces;
using CarsScrape.Model;
using CarsScraping.Infrastructure.Selenium.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace CarsScrape.Infrastructure.Config
{
    public class CarsScrapeService : ICarsScrapeService
    {
        private readonly ICarsScrapeConfig _ICarsScrapeConfig;
        private readonly ISeleniumServiceContract _Selenium;

        public CarsScrapeService(ISeleniumServiceContract pSeleniumService, ICarsScrapeConfig pCarsConfig)
        {
            _Selenium = pSeleniumService;
            _ICarsScrapeConfig = pCarsConfig;
        }

        public void SignIn()
        {
            _Selenium.Navigate(_ICarsScrapeConfig.Url + "/signin/");
            _Selenium.SetElementValueBy("id", _ICarsScrapeConfig.LoginFieldId, _ICarsScrapeConfig.UserLogin);
            _Selenium.SetElementValueBy("id", _ICarsScrapeConfig.PasswordId, _ICarsScrapeConfig.UserPassword);
            _Selenium.ClickElementBy("class", _ICarsScrapeConfig.SubmitSelector);
        }

        public void GoHome()
        {
            _Selenium.Navigate(_ICarsScrapeConfig.Url);
        }

        public List<Make_ModelCarSelenium> GetMakeCar()
        {
            //Null parameters are passed because no prior information is needed to choose makes.
            return _Selenium.GetSelectOptions("xpath", _ICarsScrapeConfig.MakeCar, null, null);
        }

        public List<Make_ModelCarSelenium> GetModelCar(string make)
        {
            //Here it's necessary pass the name of the make choosed and the selector of the make.
            return _Selenium.GetSelectOptions("xpath", _ICarsScrapeConfig.ModelCar, make, _ICarsScrapeConfig.MakeCar);
        }

        public string GetNameUser()
        {
            return _Selenium.GetNameUser("xpath", _ICarsScrapeConfig.NameUserSelector);
        }

        public IEnumerable<Car> GetCars(string make, string model)
        {
            SearchCars(make, model);
            return MountCars(make);
        }

        private void SearchCars(string make, string model)
        {
            _Selenium.ClickInSelectElementBy("xpath", _ICarsScrapeConfig.Condition, "all", false);
            _Selenium.ClickInSelectElementBy("xpath", _ICarsScrapeConfig.MakeCar, make, true);
            _Selenium.ClickInSelectElementBy("xpath", _ICarsScrapeConfig.ModelCar, model, true);
            _Selenium.ClickElementBy("xpath", _ICarsScrapeConfig.SearchButton);
        }

        private IEnumerable<Car> MountCars(string make)
        {
            //Here the car research has already been done, so the cars cards are taken.
            IEnumerable<IWebElement> vehicles = _Selenium.GetElementsBy("class", _ICarsScrapeConfig.CardVehicleSelector);
            List<Car> cars = new List<Car>();

            foreach (IWebElement item in vehicles) cars.Add(OrganizeCarStructure(item, make));
            
            return cars;
        }

        private Car OrganizeCarStructure(IWebElement item, string make)
        {
            try
            {
                string model = string.Empty;
                string concatItem = string.Empty;
                string title = item.FindElements(By.ClassName("title")).Count == 0 ? "0" : item.FindElements(By.ClassName("title"))[0].Text;

                //This structure is to get name of the model by the title and manipulation of string.
                foreach(var value in title)
                {
                    concatItem += value;
                    if (concatItem.Contains(make + " ")) 
                    { 
                        model += value; 
                        if (model[0].Equals(" ")) model.Remove(0);
                    }
                }

                //Mount the car structure.
                return new Car()
                {
                    Title = title,
                    Make = make,
                    Model = model,
                    Condition = item.FindElements(By.ClassName("stock-type")).Count == 0 ? "0" : item.FindElements(By.ClassName("stock-type"))[0].Text,
                    Mileage = item.FindElements(By.ClassName("mileage")).Count == 0 ? "0" : item.FindElements(By.ClassName("mileage"))[0].Text,
                    Value = item.FindElements(By.ClassName("primary-price")).Count == 0 ? "0" : item.FindElements(By.ClassName("primary-price"))[0].Text,
                    Rating = item.FindElements(By.ClassName("sds-rating__count")).Count == 0 ? "0" : item.FindElements(By.ClassName("sds-rating__count"))[0].Text,
                    Picture = item.FindElements(By.ClassName("vehicle-image"))[1].GetAttribute("src")
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
