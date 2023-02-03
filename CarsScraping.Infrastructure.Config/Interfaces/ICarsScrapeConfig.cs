namespace CarsScrape.Infrastructure.Config.Interfaces
{
    public interface ICarsScrapeConfig
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; } 
        public string SubmitSelector { get; set; }
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
