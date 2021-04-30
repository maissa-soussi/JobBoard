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
    public class EducationLevelsController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        EducationLevel _oEducationLevel = new EducationLevel();
        List<EducationLevel> _oEducationLevels = new List<EducationLevel>();
        public EducationLevelsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<EducationLevel>> GetEducationLevel()
        {
            _oEducationLevels = new List<EducationLevel>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/EducationLevels"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oEducationLevels = JsonConvert.DeserializeObject<List<EducationLevel>>(apiResponse);
                }
            }
            return _oEducationLevels;
        }

        [HttpGet("{Id}")]
        public async Task<EducationLevel> GetEducationLevel(int Id)
        {
            _oEducationLevel = new EducationLevel();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/EducationLevels/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oEducationLevel = JsonConvert.DeserializeObject<EducationLevel>(apiResponse);
                }
            }
            return _oEducationLevel;
        }

        [HttpPost]
        public async Task<EducationLevel> PostEducationLevel(EducationLevel EducationLevel)
        {
            _oEducationLevel = new EducationLevel();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(EducationLevel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/EducationLevels", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oEducationLevel = JsonConvert.DeserializeObject<EducationLevel>(apiResponse);
                }
            }
            return _oEducationLevel;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEducationLevel(int Id, EducationLevel EducationLevel)
        {
            _oEducationLevel = new EducationLevel();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(EducationLevel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/EducationLevels/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oEducationLevel = JsonConvert.DeserializeObject<EducationLevel>(apiResponse);
                        return Ok(_oEducationLevel);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteEducationLevel(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/EducationLevels/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
