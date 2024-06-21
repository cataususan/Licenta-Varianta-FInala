using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using System.Text.Json;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        [HttpGet("getCurrency")]
        [Authorize]
        public async Task<ActionResult<float>> GetPriceOfCurrency([FromQuery] ExchangeRequest Request) 
        {
            
            //string accessKey = Environment.GetEnvironmentVariable("FIXER_ACCESS_KEY");
            string accessKey = "api_key";
            float wantedAmount = Request.Amount;
            string url = $"http://data.fixer.io/api/latest?access_key={accessKey}&symbols={Request.WantedCurrency}";
            Dictionary<string, float> ratesReturned = new Dictionary<string, float>();
            Dictionary<string, float> amountsReturned = new Dictionary<string, float>();
            try
            {
                
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var currencyResponse = JsonSerializer.Deserialize<CurrencyResponse>(responseBody,options);
                ratesReturned.Add("EUR", currencyResponse.Rates["RON"]);
                foreach (var key in currencyResponse.Rates.Keys){
                    float currencyRate = currencyResponse.Rates["RON"] / currencyResponse.Rates[key];
                    ratesReturned.Add(key, currencyRate);
                }
                foreach (var key in ratesReturned.Keys)
                {
                    float amount = wantedAmount / ratesReturned[key];
                    amountsReturned.Add(key, amount);
                }
                var value = Request.Amount * currencyResponse.Rates["RON"];
                return StatusCode(200, new { ratesReturned, amountsReturned });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
            
        }
    }
}
