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
    public class CountriesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Country _oCountry = new Country();
        List<Country> _oCountries = new List<Country>();
        public CountriesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Country>> GetCountry()
        {
            _oCountries = new List<Country>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/Countries"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCountries = JsonConvert.DeserializeObject<List<Country>>(apiResponse);
                }
            }
            return _oCountries;
        }

        [HttpGet("{Id}")]
        public async Task<Country> GetCountry(int Id)
        {
            _oCountry = new Country();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/Countries/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCountry = JsonConvert.DeserializeObject<Country>(apiResponse);
                }
            }
            return _oCountry;
        }

        [HttpPost]
        public async Task<Country> PostCountry(Country country)
        {
            _oCountry = new Country();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44363/api/Countries", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCountry = JsonConvert.DeserializeObject<Country>(apiResponse);
                }
            }
            return _oCountry;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCountry(int Id, Country country)
        {
            _oCountry = new Country();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44363/api/Countries/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCountry = JsonConvert.DeserializeObject<Country>(apiResponse);
                        return Ok(_oCountry);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCountry(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44363/api/Countries/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
