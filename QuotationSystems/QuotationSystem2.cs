using ConsoleApp1.Models;
using System.Dynamic;

namespace ConsoleApp1.QuotationSystems
{
    public class QuotationSystem2: IQuotationSystem
    {
        public QuotationSystem2(string url, string port)
        {

        }

        public dynamic GetPrice(PriceRequest priceRequest)
        {
            var requestData = this.mapRequest(priceRequest);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 234.56M;
            response.HasPrice = true;
            response.Name = "qewtrywrh";
            response.Tax = 234.56M * 0.12M;

            return response;
        }

        private dynamic mapRequest(PriceRequest priceRequest)
        {
            dynamic systemRequest2 = new ExpandoObject();
            systemRequest2.FirstName = priceRequest.RiskData.FirstName;
            systemRequest2.LastName = priceRequest.RiskData.LastName;
            systemRequest2.Make = priceRequest.RiskData.Make;
            systemRequest2.Value = priceRequest.RiskData.Value;

            return systemRequest2;
        }

    }

}
