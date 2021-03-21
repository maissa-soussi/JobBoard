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
    public class CandidateExperiencesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        CandidateExperience _oCandidateExperience = new CandidateExperience();
        List<CandidateExperience> _oCandidateExperiences = new List<CandidateExperience>();
        public CandidateExperiencesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<CandidateExperience>> GetCandidateExperience()
        {
            _oCandidateExperiences = new List<CandidateExperience>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/CandidateExperiences"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateExperiences = JsonConvert.DeserializeObject<List<CandidateExperience>>(apiResponse);
                }
            }
            return _oCandidateExperiences;
        }

        [HttpGet("{Id}")]
        public async Task<CandidateExperience> GetCandidateExperience(int Id)
        {
            _oCandidateExperience = new CandidateExperience();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44363/api/CandidateExperiences/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateExperience = JsonConvert.DeserializeObject<CandidateExperience>(apiResponse);
                }
            }
            return _oCandidateExperience;
        }

        [HttpPost]
        public async Task<CandidateExperience> PostCandidateExperience(CandidateExperience candidateExperience)
        {
            _oCandidateExperience = new CandidateExperience();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(candidateExperience), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44363/api/CandidateExperiences", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateExperience = JsonConvert.DeserializeObject<CandidateExperience>(apiResponse);
                }
            }
            return _oCandidateExperience;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCandidateExperience(int Id, CandidateExperience candidateExperience)
        {
            _oCandidateExperience = new CandidateExperience();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(candidateExperience), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44363/api/CandidateExperiences/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCandidateExperience = JsonConvert.DeserializeObject<CandidateExperience>(apiResponse);
                        return Ok(_oCandidateExperience);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCandidateExperience(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44363/api/CandidateExperiences/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
