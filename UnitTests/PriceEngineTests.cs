using System;
using NUnit.Framework;

using ConsoleApp1;
using ConsoleApp1.Models;
using static ConsoleApp1.Errors.Constants;
using System.Collections.Generic;
using ConsoleApp1.QuotationSystems;

namespace UnitTests
{
    [TestFixture]
    public class PriceEngine_test
    {
        private PriceEngine priceEngine;

        // from http://www.smaclellan.com/posts/parameterized-tests-made-simple/
        private static IEnumerable<TestCaseData> RiskDataErrorCases()
        {
            yield return new TestCaseData(null, PriceRequestErrors.Risk_Data_Missing);
            yield return new TestCaseData(new RiskData()
            {
                DOB = DateTime.Parse("1980-01-01"),
                FirstName = null,
                LastName = "Smith",
                Make = "Cool New Phone",
                Value = 500
            }, PriceRequestErrors.First_Name_Required);
            yield return new TestCaseData(new RiskData()
            {
                DOB = DateTime.Parse("1980-01-01"),
                FirstName = "John",
                LastName = null,
                Make = "Cool New Phone",
                Value = 500
            }, PriceRequestErrors.Surname_Required);
            yield return new TestCaseData(new RiskData()
            {
                DOB = DateTime.Parse("1980-01-01"),
                FirstName = "John",
                LastName = "Smith",
                Make = "Cool New Phone",
                Value = 0
            }, PriceRequestErrors.Value_Required);
        }

        private static IEnumerable<TestCaseData> RiskDataCases()
        {
            yield return new TestCaseData(new RiskData()
            {
                DOB = null,
                FirstName = "John",
                LastName = "Smith",
                Make = "Cool New Phone",
                Value = 500
            }, new PriceResponse()
            {
                Price = (92.67M),
                Tax = (92.67M * 0.12M),
                InsurerName = "zxcvbnm"
            }, string.Empty);

            yield return new TestCaseData(new RiskData()
            {
                DOB = DateTime.Parse("1980-01-01"),
                FirstName = "John",
                LastName = "Smith",
                Make = "Cool New Phone",
                Value = 500
            }, new PriceResponse()
            {
                Price = (92.67M),
                Tax = (92.67M * 0.12M),
                InsurerName = "zxcvbnm"
            }, string.Empty);

            yield return new TestCaseData(new RiskData()
            {
                DOB = DateTime.Parse("1980-01-01"),
                FirstName = "John",
                LastName = "Smith",
                Make = "examplemake1",
                Value = 500
            }, new PriceResponse()
            {
                Price = (92.67M),
                Tax = (92.67M * 0.12M),
                InsurerName = "zxcvbnm"
            }, string.Empty);
        }

        [SetUp]
        public void Setup()
        {
            priceEngine = new PriceEngine();
        }

        [Test, TestCaseSource("RiskDataErrorCases")]
        public void ShouldReturnErrorWhenRiskDataErrors(RiskData data, string expectedMessage)
        {
            var request = new PriceRequest()
            {
                RiskData = data
            };

            var errorResult = string.Empty;
            priceEngine.GetPrice(request, out errorResult);

            Assert.AreEqual(expectedMessage, errorResult);
        }

        [Test, TestCaseSource("RiskDataCases")]
        public void ShouldReturnResponseWithoutError(RiskData data, PriceResponse expectedResponse, string expectedMessage)
        {
            var request = new PriceRequest()
            {
                RiskData = data
            };

            var error = string.Empty;
            var result = priceEngine.GetPrice(request, out error);

            Assert.AreEqual(expectedMessage, error);

            Assert.AreEqual(expectedResponse.Price, result.Price);
            Assert.AreEqual(expectedResponse.Tax, result.Tax);
            Assert.AreEqual(expectedResponse.InsurerName, result.InsurerName);
        }
    }
}