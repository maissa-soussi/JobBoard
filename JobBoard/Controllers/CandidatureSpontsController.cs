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
    public class CandidatureSpontsController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        CandidatureSpont _oCandidatureSpont = new CandidatureSpont();
        List<CandidatureSpont> _oCandidatureSponts = new List<CandidatureSpont>();
        public CandidatureSpontsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<CandidatureSpont>> GetCandidatureSpont()
        {
            _oCandidatureSponts = new List<CandidatureSpont>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/CandidatureSponts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidatureSponts = JsonConvert.DeserializeObject<List<CandidatureSpont>>(apiResponse);
                }
            }
            return _oCandidatureSponts;
        }

        [HttpGet("{Id}")]
        public async Task<CandidatureSpont> GetCandidatureSpont(int Id)
        {
            _oCandidatureSpont = new CandidatureSpont();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/CandidatureSponts/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidatureSpont = JsonConvert.DeserializeObject<CandidatureSpont>(apiResponse);
                }
            }
            return _oCandidatureSpont;
        }

        [HttpPost]
        public async Task<CandidatureSpont> PostCandidatureSpont(CandidatureSpont CandidatureSpont)
        {
            _oCandidatureSpont = new CandidatureSpont();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(CandidatureSpont), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/CandidatureSponts", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidatureSpont = JsonConvert.DeserializeObject<CandidatureSpont>(apiResponse);
                }
            }
            return _oCandidatureSpont;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCandidatureSpont(int Id, CandidatureSpont CandidatureSpont)
        {
            _oCandidatureSpont = new CandidatureSpont();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(CandidatureSpont), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/CandidatureSponts/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCandidatureSpont = JsonConvert.DeserializeObject<CandidatureSpont>(apiResponse);
                        return Ok(_oCandidatureSpont);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCandidatureSpont(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/CandidatureSponts/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}

