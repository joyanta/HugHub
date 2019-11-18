using System;
namespace ConsoleApp1.Models
{
    public class PriceResponse
    {
        public decimal Price { get; set; } = Decimal.MaxValue;
        public decimal? Tax { get; set; }
        public string InsurerName { get; set; }
    }
}

