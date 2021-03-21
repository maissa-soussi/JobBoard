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
    public class DomainsController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Domain _oDomain = new Domain();
        List<Domain> _oDomains = new List<Domain>();
        public DomainsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Domain>> GetDomain()
        {
            _oDomains = new List<Domain>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/Domains"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDomains = JsonConvert.DeserializeObject<List<Domain>>(apiResponse);
                }
            }
            return _oDomains;
        }

        [HttpGet("{Id}")]
        public async Task<Domain> GetDomain(int Id)
        {
            _oDomain = new Domain();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/Domains/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDomain = JsonConvert.DeserializeObject<Domain>(apiResponse);
                }
            }
            return _oDomain;
        }

        [HttpPost]
        public async Task<Domain> PostDomain(Domain domain)
        {
            _oDomain = new Domain();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(domain), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44363/api/Domains", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDomain = JsonConvert.DeserializeObject<Domain>(apiResponse);
                }
            }
            return _oDomain;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutDomain(int Id, Domain domain)
        {
            _oDomain = new Domain();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(domain), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44363/api/Domains/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oDomain = JsonConvert.DeserializeObject<Domain>(apiResponse);
                        return Ok(_oDomain);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteDomain(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44363/api/Domains/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
