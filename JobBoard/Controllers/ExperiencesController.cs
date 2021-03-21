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
    public class ExperiencesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Experience _oExperience = new Experience();
        List<Experience> _oExperiences = new List<Experience>();
        public ExperiencesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Experience>> GetExperience()
        {
            _oExperiences = new List<Experience>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/Experiences"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oExperiences = JsonConvert.DeserializeObject<List<Experience>>(apiResponse);
                }
            }
            return _oExperiences;
        }

        [HttpGet("{Id}")]
        public async Task<Experience> GetExperience(int Id)
        {
            _oExperience = new Experience();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/Experiences/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oExperience = JsonConvert.DeserializeObject<Experience>(apiResponse);
                }
            }
            return _oExperience;
        }

        [HttpPost]
        public async Task<Experience> PostExperience(Experience experience)
        {
            _oExperience = new Experience();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(experience), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44363/api/Experiences", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oExperience = JsonConvert.DeserializeObject<Experience>(apiResponse);
                }
            }
            return _oExperience;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutExperience(int Id, Experience experience)
        {
            _oExperience = new Experience();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(experience), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44363/api/Experiences/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oExperience = JsonConvert.DeserializeObject<Experience>(apiResponse);
                        return Ok(_oExperience);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteExperience(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44363/api/Experiences/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
