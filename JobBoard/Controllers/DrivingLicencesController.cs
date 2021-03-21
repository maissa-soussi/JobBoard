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
    [Route("api/[controller]")]
    [ApiController]
    public class DrivingLicencesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        DrivingLicence _oDrivingLicence = new DrivingLicence();
        List<DrivingLicence> _oDrivingLicences = new List<DrivingLicence>();
        public DrivingLicencesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<DrivingLicence>> GetDrivingLicence()
        {
            _oDrivingLicences = new List<DrivingLicence>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/DrivingLicences"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDrivingLicences = JsonConvert.DeserializeObject<List<DrivingLicence>>(apiResponse);
                }
            }
            return _oDrivingLicences;
        }

        [HttpGet("{Id}")]
        public async Task<DrivingLicence> GetDrivingLicence(int Id)
        {
            _oDrivingLicence = new DrivingLicence();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/DrivingLicences/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDrivingLicence = JsonConvert.DeserializeObject<DrivingLicence>(apiResponse);
                }
            }
            return _oDrivingLicence;
        }

        [HttpPost]
        public async Task<DrivingLicence> PostDrivingLicence(DrivingLicence drivingLicence)
        {
            _oDrivingLicence = new DrivingLicence();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(drivingLicence), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/DrivingLicences", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDrivingLicence = JsonConvert.DeserializeObject<DrivingLicence>(apiResponse);
                }
            }
            return _oDrivingLicence;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutDrivingLicence(int Id, DrivingLicence drivingLicence)
        {
            _oDrivingLicence = new DrivingLicence();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(drivingLicence), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/DrivingLicences/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oDrivingLicence = JsonConvert.DeserializeObject<DrivingLicence>(apiResponse);
                        return Ok(_oDrivingLicence);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteDrivingLicence(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/DrivingLicences/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
