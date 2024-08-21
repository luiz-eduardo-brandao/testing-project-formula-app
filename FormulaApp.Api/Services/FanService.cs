using System.Net;
using FormulaApp.Api.Configuration;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FormulaApp.Api.Services
{
    public class FanService : IFanService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiServiceConfig _apiCongig;

        public FanService(HttpClient httpClient, IOptions<ApiServiceConfig> config)
        {
            _httpClient = httpClient;
            _apiCongig = config.Value;
        }

        public async Task<List<Fan>?> GetAllFans() 
        {
            var response = await _httpClient.GetAsync(_apiCongig.Url);
            
            switch (response.StatusCode) 
            {
                case HttpStatusCode.NotFound:
                    return new List<Fan>();
                case HttpStatusCode.Unauthorized:
                    return null;
                default:
                {
                    var fans = await response.Content.ReadFromJsonAsync<List<Fan>>() ?? new List<Fan>();
                    return fans;
                }
            }
        }
    }
}