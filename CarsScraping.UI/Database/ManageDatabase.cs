using CarsScrape.Model;
using CarsScraping.UI.Context;
using System.Collections.Generic;
using System.Linq;

namespace CarsScraping.UI.Database
{
    public class ManageDatabase
    {
        private CarsContext _carsContext = new CarsContext();

        internal void CreateDataBase()
        {
            _carsContext.Database.EnsureDeleted();
            _carsContext.Database.EnsureCreated();
        }


        internal void AddCarsToDatabase(IEnumerable<Car> cars)
        {   
            _carsContext.Cars.AddRange(cars);
            _carsContext.SaveChanges();
        }

        internal List<Car> GetCars()
        {
            return _carsContext.Cars.ToList();
        }
    }
}