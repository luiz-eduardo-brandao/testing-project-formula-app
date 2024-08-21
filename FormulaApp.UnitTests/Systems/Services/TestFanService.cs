using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormulaApp.Api.Configuration;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services;
using FormulaApp.UnitTests.Fixtures;
using FormulaApp.UnitTests.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace FormulaApp.UnitTests.Systems.Services
{
    public class TestFanService
    {
        [Fact]
        public async Task GetAllFans_OnInvoked_HttpGet()
        {
            // Arrange
            var url = "https://mywebsite.com/api/v1/fans";

            var response = FansFixtures.GetFans();
            var mockHandler = MockHttpHandler<Fan>.SetupGetResquest(response);
            var httpClient = new HttpClient(mockHandler.Object);

            var apiConfig = Options.Create(new ApiServiceConfig() 
            {
                Url = url,
            });
            
            var fanService = new FanService(httpClient, apiConfig);

            // Act
            await fanService.GetAllFans();

            // Assert
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(r => 
                    r.Method == HttpMethod.Get && r.RequestUri.ToString() == url),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}