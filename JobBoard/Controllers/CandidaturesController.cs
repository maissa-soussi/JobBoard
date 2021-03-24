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
    public class CandidaturesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Candidature _oCandidature = new Candidature();
        List<Candidature> _oCandidatures = new List<Candidature>();
        public CandidaturesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Candidature>> GetCandidature()
        {
            _oCandidatures = new List<Candidature>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Candidatures"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidatures = JsonConvert.DeserializeObject<List<Candidature>>(apiResponse);
                }
            }
            return _oCandidatures;
        }

        [HttpGet("{Id}")]
        public async Task<Candidature> GetCandidature(int Id)
        {
            _oCandidature = new Candidature();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Candidatures/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidature = JsonConvert.DeserializeObject<Candidature>(apiResponse);
                }
            }
            return _oCandidature;
        }

        [HttpPost]
        public async Task<Candidature> PostCandidature(Candidature Candidature)
        {
            _oCandidature = new Candidature();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Candidature), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Candidatures", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidature = JsonConvert.DeserializeObject<Candidature>(apiResponse);
                }
            }
            return _oCandidature;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCandidature(int Id, Candidature Candidature)
        {
            _oCandidature = new Candidature();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Candidature), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Candidatures/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCandidature = JsonConvert.DeserializeObject<Candidature>(apiResponse);
                        return Ok(_oCandidature);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCandidature(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Candidatures/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}