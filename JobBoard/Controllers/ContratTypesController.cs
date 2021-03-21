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
    public class ContratTypesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        ContratType _oContratType = new ContratType();
        List<ContratType> _oContratTypes = new List<ContratType>();
        public ContratTypesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<ContratType>> GetContratType()
        {
            _oContratTypes = new List<ContratType>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/ContratTypes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oContratTypes = JsonConvert.DeserializeObject<List<ContratType>>(apiResponse);
                }
            }
            return _oContratTypes;
        }

        [HttpGet("{Id}")]
        public async Task<ContratType> GetContratType(int Id)
        {
            _oContratType = new ContratType();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/ContratTypes/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oContratType = JsonConvert.DeserializeObject<ContratType>(apiResponse);
                }
            }
            return _oContratType;
        }

        [HttpPost]
        public async Task<ContratType> PostContratType(ContratType contratType)
        {
            _oContratType = new ContratType();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(contratType), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/ContratTypes", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oContratType = JsonConvert.DeserializeObject<ContratType>(apiResponse);
                }
            }
            return _oContratType;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutContratType(int Id, ContratType contratType)
        {
            _oContratType = new ContratType();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(contratType), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/ContratTypes/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oContratType = JsonConvert.DeserializeObject<ContratType>(apiResponse);
                        return Ok(_oContratType);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteContratType(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/ContratTypes/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
