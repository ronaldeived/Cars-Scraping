using System.ComponentModel.DataAnnotations;

namespace CarsScrape.Model
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Condition { get; set; }
        public string Mileage { get; set; }
        public string Value { get; set; }
        public string Rating { get; set; }
        public string Picture { get; set; }

    }
}
