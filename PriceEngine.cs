using System;
using System.Collections.Generic;
using ConsoleApp1.Models;
using ConsoleApp1.QuotationSystems;
using static ConsoleApp1.Errors.Constants;

namespace ConsoleApp1
{
    public class PriceEngine
    {
        //pass request with risk data with details of a gadget, return the best price retrieved from 3 external quotation engines
        public PriceResponse GetPrice(PriceRequest request, out string errorMessage)
        {
            errorMessage = string.Empty;

            var result = new PriceResponse();

            this.ValidateRequest(request, ref errorMessage);
            if (errorMessage.Length > 0)
            {
                return result;
            }

            //now call 3 external system and get the best price
            foreach (var quoteSys in this.GetQuotationSystems(request))
            {
                var systemResponse = quoteSys.GetPrice(request);
                if (this.IsResponseValid(systemResponse) && systemResponse.Price < result.Price)
                {
                    result.Price = Math.Min(systemResponse.Price, result.Price);
                    result.InsurerName = systemResponse.Name;
                    result.Tax = systemResponse.Tax;
                }
            }

            return result;
        }

        private void ValidateRequest(PriceRequest request, ref string errorMessage)
        {
            if (request.RiskData == null)
            {
                errorMessage = PriceRequestErrors.Risk_Data_Missing;
                return;
            } 
           
            if (string.IsNullOrEmpty(request.RiskData.FirstName))
            {
                errorMessage = PriceRequestErrors.First_Name_Required;
                return;
            }

            if (string.IsNullOrEmpty(request.RiskData.LastName))
            {
                errorMessage = PriceRequestErrors.Surname_Required;
                return;
            }

            if (request.RiskData.Value == 0)
            {
                errorMessage = PriceRequestErrors.Value_Required;
                return;
            }
        }
        private bool IsResponseValid(dynamic systemResponse)
        {
            return 
                Lib.HasProperty(systemResponse, "IsSuccess") && systemResponse.IsSuccess
                || Lib.HasProperty(systemResponse, "HasPrice") && systemResponse.HasPrice;
        }

        private IList<IQuotationSystem> GetQuotationSystems(PriceRequest request)
        {
            var quotationSystems = new List<IQuotationSystem>();

            if (this.IsValidMakeForSystem1(request))
            {
                quotationSystems.Add(new QuotationSystem1("http://quote-system-1.com", "1234"));
            }

            if (this.IsValidMakeForSystem2(request))
            {
                quotationSystems.Add(new QuotationSystem2("http://quote-system-2.com", "1235"));
            }

            quotationSystems.Add(new QuotationSystem3("http://quote-system-3.com", "100"));

            return quotationSystems;
        }

        private bool IsValidMakeForSystem1(PriceRequest request)
        {
            return request.RiskData.DOB.HasValue;
        }

        private bool IsValidMakeForSystem2(PriceRequest request)
        {
            var makeList = new List<string> { "examplemake1", "examplemake2", "examplemake3" };

            return makeList.IndexOf(request.RiskData.Make) >= 0;
        }

    }
}
