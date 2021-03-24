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
    public class DiplomasController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Diploma _oDiploma = new Diploma();
        List<Diploma> _oDiplomas = new List<Diploma>();
        public DiplomasController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Diploma>> GetDiploma()
        {
            _oDiplomas = new List<Diploma>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Diplomas"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDiplomas = JsonConvert.DeserializeObject<List<Diploma>>(apiResponse);
                }
            }
            return _oDiplomas;
        }

        [HttpGet("{Id}")]
        public async Task<Diploma> GetDiploma(int Id)
        {
            _oDiploma = new Diploma();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Diplomas/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDiploma = JsonConvert.DeserializeObject<Diploma>(apiResponse);
                }
            }
            return _oDiploma;
        }

        [HttpPost]
        public async Task<Diploma> PostDiploma(Diploma Diploma)
        {
            _oDiploma = new Diploma();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Diploma), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Diplomas", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDiploma = JsonConvert.DeserializeObject<Diploma>(apiResponse);
                }
            }
            return _oDiploma;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutDiploma(int Id, Diploma Diploma)
        {
            _oDiploma = new Diploma();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Diploma), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Diplomas/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oDiploma = JsonConvert.DeserializeObject<Diploma>(apiResponse);
                        return Ok(_oDiploma);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteDiploma(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Diplomas/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
