using System;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //SNIP - collect input (risk data from the user)

            var request = new PriceRequest()
            {
                RiskData = new RiskData() //hardcoded here, but would normally be from user input above
                {
                    DOB = DateTime.Parse("1980-01-01"),
                    FirstName = "John",
                    LastName = "Smith",
                    Make = "Cool New Phone",
                    Value = 500
                }
            };

            var priceEngine = new PriceEngine();
            var error = string.Empty;
            var priceResponse = priceEngine.GetPrice(request, out error);

            if (priceResponse.Price == Decimal.MaxValue)
            {
                Console.WriteLine(String.Format("There was an error - {0}", error));
            }
            else
            {
                Console.WriteLine(String.Format("You price is {0}, from insurer: {1}. This includes tax of {2}", 
                    priceResponse.Price, priceResponse.InsurerName, priceResponse.Tax));
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
