using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JobBoard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobBoard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Status _oStatus = new Status();
        List<Status> _oStatuses = new List<Status>();
        public StatusController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Status>> GetStatus()
        {
            _oStatuses = new List<Status>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Status"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStatuses = JsonConvert.DeserializeObject<List<Status>>(apiResponse);
                }
            }
            return _oStatuses;
        }

        [HttpGet("{Id}")]
        public async Task<Status> GetStatus(int Id)
        {
            _oStatus = new Status();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Status/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStatus = JsonConvert.DeserializeObject<Status>(apiResponse);
                }
            }
            return _oStatus;
        }

        [HttpPost]
        public async Task<Status> PostStatus(Status Status)
        {
            _oStatus = new Status();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Status), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Status", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStatus = JsonConvert.DeserializeObject<Status>(apiResponse);
                }
            }
            return _oStatus;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutStatus(int Id, Status Status)
        {
            _oStatus = new Status();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Status), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Status/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oStatus = JsonConvert.DeserializeObject<Status>(apiResponse);
                        return Ok(_oStatus);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteStatus(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Status/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
