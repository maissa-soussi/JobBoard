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
    public class CandidateLanguagesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        CandidateLanguage _oCandidateLanguage = new CandidateLanguage();
        List<CandidateLanguage> _oCandidateLanguages = new List<CandidateLanguage>();
        public CandidateLanguagesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<CandidateLanguage>> GetCandidateLanguage()
        {
            _oCandidateLanguages = new List<CandidateLanguage>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/CandidateLanguages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateLanguages = JsonConvert.DeserializeObject<List<CandidateLanguage>>(apiResponse);
                }
            }
            return _oCandidateLanguages;
        }

        [HttpGet("{Id}")]
        public async Task<CandidateLanguage> GetCandidateLanguage(int Id)
        {
            _oCandidateLanguage = new CandidateLanguage();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/CandidateLanguages/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateLanguage = JsonConvert.DeserializeObject<CandidateLanguage>(apiResponse);
                }
            }
            return _oCandidateLanguage;
        }

        [HttpPost]
        public async Task<CandidateLanguage> PostCandidateLanguage(CandidateLanguage candidateLanguage)
        {
            _oCandidateLanguage = new CandidateLanguage();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(candidateLanguage), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/CandidateLanguages", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateLanguage = JsonConvert.DeserializeObject<CandidateLanguage>(apiResponse);
                }
            }
            return _oCandidateLanguage;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCandidateLanguage(int Id, CandidateLanguage candidateLanguage)
        {
            _oCandidateLanguage = new CandidateLanguage();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(candidateLanguage), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/CandidateLanguages/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCandidateLanguage = JsonConvert.DeserializeObject<CandidateLanguage>(apiResponse);
                        return Ok(_oCandidateLanguage);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCandidateLanguage(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/CandidateLanguages/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
