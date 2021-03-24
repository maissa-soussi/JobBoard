using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using JobBoard.Models;

namespace JobBoard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Currency _oCurrency = new Currency();
        List<Currency> _oCurrencies = new List<Currency>();
        public CurrenciesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Currency>> GetCurrency()
        {
            _oCurrencies = new List<Currency>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Currencies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCurrencies = JsonConvert.DeserializeObject<List<Currency>>(apiResponse);
                }
            }
            return _oCurrencies;
        }

        [HttpGet("{Id}")]
        public async Task<Currency> GetCurrency(int Id)
        {
            _oCurrency = new Currency();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Currencies/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCurrency = JsonConvert.DeserializeObject<Currency>(apiResponse);
                }
            }
            return _oCurrency;
        }

        [HttpPost]
        public async Task<Currency> PostCurrency(Currency currency)
        {
            _oCurrency = new Currency();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(currency), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Currencies", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCurrency = JsonConvert.DeserializeObject<Currency>(apiResponse);
                }
            }
            return _oCurrency;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCurrency(int Id, Currency currency)
        {
            _oCurrency = new Currency();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(currency), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Currencies/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCurrency = JsonConvert.DeserializeObject<Currency>(apiResponse);
                        return Ok(_oCurrency);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCurrency(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Currencies/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
