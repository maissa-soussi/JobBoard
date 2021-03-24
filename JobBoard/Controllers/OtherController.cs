using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using JobBoard.Models;
using System.Text;

namespace JobBoard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Other _oOther = new Other();
        List<Other> _oOthers = new List<Other>();
        public OtherController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Other>> GetOther()
        {
            _oOthers = new List<Other>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Other"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oOthers = JsonConvert.DeserializeObject<List<Other>>(apiResponse);
                }
            }
            return _oOthers;
        }

        [HttpGet("{Id}")]
        public async Task<Other> GetOther(int Id)
        {
            _oOther = new Other();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Other/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oOther = JsonConvert.DeserializeObject<Other>(apiResponse);
                }
            }
            return _oOther;
        }

        [HttpPost]
        public async Task<Other> PostOther(Other other)
        {
            _oOther = new Other();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(other), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Other", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oOther = JsonConvert.DeserializeObject<Other>(apiResponse);
                }
            }
            return _oOther;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutOther(int Id, Other other)
        {
            _oOther = new Other();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(other), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Other/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oOther = JsonConvert.DeserializeObject<Other>(apiResponse);
                        return Ok(_oOther);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteOther(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Other/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
