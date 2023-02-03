using CarsScraping.UI;

namespace CarsScrape.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManageCalls manageCalls = new ManageCalls();

            manageCalls.Load();

            manageCalls.SayGoodBye();
        }
    }
}
