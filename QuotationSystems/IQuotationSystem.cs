using ConsoleApp1.Models;
namespace ConsoleApp1.QuotationSystems
{
    public interface IQuotationSystem
    {
        dynamic GetPrice(PriceRequest request);
    }
}

