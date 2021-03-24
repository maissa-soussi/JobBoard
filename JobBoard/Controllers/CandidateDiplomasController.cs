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
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateDiplomasController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        CandidateDiploma _oCandidateDiploma = new CandidateDiploma();
        List<CandidateDiploma> _oCandidateDiplomas = new List<CandidateDiploma>();
        public CandidateDiplomasController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<CandidateDiploma>> GetCandidateDiploma()
        {
            _oCandidateDiplomas = new List<CandidateDiploma>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/CandidateDiplomas"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateDiplomas = JsonConvert.DeserializeObject<List<CandidateDiploma>>(apiResponse);
                }
            }
            return _oCandidateDiplomas;
        }

        [HttpGet("{Id}")]
        public async Task<CandidateDiploma> GetCandidateDiploma(int Id)
        {
            _oCandidateDiploma = new CandidateDiploma();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/CandidateDiplomas/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateDiploma = JsonConvert.DeserializeObject<CandidateDiploma>(apiResponse);
                }
            }
            return _oCandidateDiploma;
        }

        [HttpPost]
        public async Task<CandidateDiploma> PostCandidateDiploma(CandidateDiploma CandidateDiploma)
        {
            _oCandidateDiploma = new CandidateDiploma();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(CandidateDiploma), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/CandidateDiplomas", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidateDiploma = JsonConvert.DeserializeObject<CandidateDiploma>(apiResponse);
                }
            }
            return _oCandidateDiploma;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCandidateDiploma(int Id, CandidateDiploma CandidateDiploma)
        {
            _oCandidateDiploma = new CandidateDiploma();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(CandidateDiploma), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/CandidateDiplomas/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oCandidateDiploma = JsonConvert.DeserializeObject<CandidateDiploma>(apiResponse);
                        return Ok(_oCandidateDiploma);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteCandidateDiploma(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/CandidateDiplomas/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}