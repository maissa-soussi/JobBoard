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
    public class CandidatesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Candidate _oCandidate = new Candidate();
        List<Candidate> _oCandidates = new List<Candidate>();
        public CandidatesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Candidate>> GetCandidate()
        {
            _oCandidates = new List<Candidate>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Candidates"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidates = JsonConvert.DeserializeObject<List<Candidate>>(apiResponse);
                }
            }
            return _oCandidates;
        }

        [HttpGet("{Id}")]
        public async Task<Candidate> GetCandidate(int Id)
        {
            _oCandidate = new Candidate();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Candidates/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidate = JsonConvert.DeserializeObject<Candidate>(apiResponse);
                }
            }
            return _oCandidate;
        }

        [HttpPost]
        public async Task<Candidate> PostCandidate(Candidate candidate)
        {
            _oCandidate = new Candidate();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(candidate), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Candidates", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidate = JsonConvert.DeserializeObject<Candidate>(apiResponse);
                }
            }
            return _oCandidate;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCandidate(int Id, Candidate candidate)
        {
            _oCandidate = new Candidate();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(candidate), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Candidates/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCandidate = JsonConvert.DeserializeObject<Candidate>(apiResponse);
                        return Ok(_oCandidate);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCandidate(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Candidates/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
