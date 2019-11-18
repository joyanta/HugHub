using ConsoleApp1.Models;
using System.Dynamic;
namespace ConsoleApp1.QuotationSystems
{
    public class QuotationSystem3: IQuotationSystem
    {
        public QuotationSystem3(string url, string port)
        {

        }

        public dynamic GetPrice(PriceRequest request)
        {
            var requestData = this.mapRequest(request);

            //makes a call to an external service - SNIP
            //var response = _someExternalService.PostHttpRequest(requestData);

            dynamic response = new ExpandoObject();
            response.Price = 92.67M;
            response.IsSuccess = true;
            response.Name = "zxcvbnm";
            response.Tax = 92.67M * 0.12M;

            return response;
        }

        private dynamic mapRequest(PriceRequest priceRequest)
        {
            dynamic systemRequest3 = new ExpandoObject();
            systemRequest3.FirstName = priceRequest.RiskData.FirstName;
            systemRequest3.Surname = priceRequest.RiskData.LastName;
            systemRequest3.DOB = priceRequest.RiskData.DOB;
            systemRequest3.Make = priceRequest.RiskData.Make;
            systemRequest3.Amount = priceRequest.RiskData.Value;

            return systemRequest3;
        }
    }
}
