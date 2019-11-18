using System.Dynamic;
using ConsoleApp1.Models;

namespace ConsoleApp1.QuotationSystems
{
    public class QuotationSystem1: IQuotationSystem
    {
        public QuotationSystem1(string url, string port)
        {
        }
     
        public dynamic GetPrice(PriceRequest request)
        {
            var requestData = this.mapRequest(request);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);


            dynamic response = new ExpandoObject();
            response.Price = 123.45M;
            response.IsSuccess = true;
            response.Name = "Test Name";
            response.Tax = 123.45M * 0.12M;

            return response;
        }

        private dynamic mapRequest(PriceRequest priceRequest)
        {
            dynamic systemRequest1 = new ExpandoObject();
            systemRequest1.FirstName = priceRequest.RiskData.FirstName;
            systemRequest1.Surname = priceRequest.RiskData.LastName;
            systemRequest1.DOB = priceRequest.RiskData.DOB;
            systemRequest1.Make = priceRequest.RiskData.Make;
            systemRequest1.Amount = priceRequest.RiskData.Value;

            return systemRequest1;
        }

    }
}
