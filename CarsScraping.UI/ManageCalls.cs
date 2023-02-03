using CarsScrape.Infrastructure.Config.Interfaces;
using CarsScrape.Infrastructure.DependencyInjection;
using CarsScrape.Model;
using CarsScraping.Infrastructure.Selenium.Model;
using CarsScraping.UI.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace CarsScraping.UI
{
    internal class ManageCalls
    {
        private ManageDatabase database = new ManageDatabase();
        private ICarsScrapeService carsScrapingService;
        private MountDependencies DI = new MountDependencies();

        internal void SayGoodBye()
        {
            Console.Clear();
            Console.WriteLine("Thank You for use this application =D");
            Thread.Sleep(5000);
            Environment.Exit(0);

        }

        internal void Load()
        {
            //This structure is for creating the database
            Console.WriteLine("Starting Creation of Database");
            database.CreateDataBase();
            Console.WriteLine("Finished Creation of Database");

            //This structure is for sigin on cars.com
            Console.WriteLine("Starting Signin on Cars.com");
            carsScrapingService = DI.CarsScraping();

            carsScrapingService.SignIn();
            Console.WriteLine("Finished Signin on Cars");

            Thread.Sleep(2000);
            Console.Clear();
            string nameUser = carsScrapingService.GetNameUser();
            Console.WriteLine($"Welcome {nameUser}");
            Thread.Sleep(2000);

            //This is where every question structure is created!
            AskTheQuestions(carsScrapingService);
        }

        private IEnumerable<Car> SearchCars(ICarsScrapeService carsScrapeService)
        {
            Make_ModelCarSelenium makesModel = new Make_ModelCarSelenium();
            Console.WriteLine("Starting Scraping Cars");
            
            Console.WriteLine("Please choose a car make option!");
            makesModel.Make = ChooseMakeModelCar("Make", null).Text; //This method is called both for choosing the make and model of the car. Changing only by an if condition.


            Console.WriteLine("Please choose a car model option!");
            makesModel.Model = ChooseMakeModelCar("Model", makesModel.Make).Text; //This method is called both for choosing the make and model of the car. Changing only by an if condition.


            IEnumerable<Car> result = carsScrapeService.GetCars(makesModel.Make, makesModel.Model);
            Console.WriteLine("Finished Scraping");

            return result;
        }

        private Make_ModelCarSelenium ChooseMakeModelCar(string whichSearch, string? make)
        {
            List<Make_ModelCarSelenium> makesModel = new List<Make_ModelCarSelenium>();
            List<Make_ModelCarSelenium> copyMakes = new List<Make_ModelCarSelenium>();

            //Verify if is requested take the Make or the Model of the car.
            if(whichSearch.Equals("Make")) makesModel = carsScrapingService.GetMakeCar();
            else makesModel = carsScrapingService.GetModelCar(make);

            copyMakes.AddRange(makesModel);

            //Where is showed to user choose the make or the model the car
            for (int i = 0; i < makesModel.Count; i++) Console.WriteLine($"{i} to choose the {whichSearch} of the Car - {makesModel[i].Text}");

            var responseMake = Console.ReadLine().Trim();
            bool hasLetter = false;

            //This verifications bellow is to check is the answer of the user is right.
            foreach (var item in responseMake) hasLetter =  char.IsLetter(item);

            if (responseMake.ToString() != null && !responseMake.ToString().Equals("0") && !responseMake.ToString().Equals("0") && !hasLetter)
            {
                makesModel.RemoveAll(a => a.Text != null);
                //filter the right answer of the user.
                makesModel.Add(copyMakes[Int32.Parse(responseMake, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite)]);
            }

            //Here is to do almost the same thing as above, but it's changing the sentence, the user chooses the wrong answer.
            while (makesModel.Count == 0)
            {
                makesModel = copyMakes;
                Console.Clear();
                Console.WriteLine("You choose a wrong value. Please choose a right value!");
                for (int i = 0; i < makesModel.Count; i++) Console.WriteLine($"{i} to choose the {whichSearch} of the Car - {makesModel[i].Text}");
                responseMake = Console.ReadLine().Trim();

                hasLetter = false;
                foreach (var item in responseMake) hasLetter = char.IsLetter(item);
                if (responseMake.ToString() != null && !responseMake.ToString().Equals("0") && !responseMake.ToString().Equals("0") && !hasLetter)
                {
                    makesModel.RemoveAll(a => a.Text != null);
                    makesModel.Add(copyMakes[Int32.Parse(responseMake, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite)]);
                }
            }

            Console.Clear();
            Console.WriteLine($"You choose the {whichSearch} of the Car - {makesModel[0].Text}");

            return makesModel[0];
        }

        private void SaveCar(IEnumerable<Car> result)
        {
            Console.WriteLine("Starting Adding in Database");
            database.AddCarsToDatabase(result);
            Console.WriteLine("Finished Proccess to add in database");
        }

        private void AskTheQuestions(ICarsScrapeService carsScrapeService)
        {
            var continueAsking = true;

            while (continueAsking)
            {
                //Search car make and model, and show the user to choose.
                List<Car> result = (List<Car>) SearchCars(carsScrapeService);

                //Saves in the database if the result is greater than zero.
                if (result.Count > 0) SaveCar(result);
                else Console.WriteLine("We don't get results in this search!");

                Console.WriteLine("Do you want to keep searching for cars and saving them?");
                Console.WriteLine("1 - Yes");
                Console.WriteLine("2 - No");
                var response = Console.ReadLine();

                if (response.Equals("1"))
                {
                    continueAsking = true;
                    carsScrapeService.GoHome();
                }
                else if (response.Equals("2")) continueAsking = false;
                else
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("You have chosen an option other than those requested.");
                    Console.WriteLine("So the application was terminated.");
                    continueAsking = false;
                }
            }
        }
    }
}
