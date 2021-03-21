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
    public class LanguageLevelsController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        LanguageLevel _oLanguageLevel = new LanguageLevel();
        List<LanguageLevel> _oLanguageLevels = new List<LanguageLevel>();
        public LanguageLevelsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<LanguageLevel>> GetLanguageLevel()
        {
            _oLanguageLevels = new List<LanguageLevel>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/LanguageLevels"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oLanguageLevels = JsonConvert.DeserializeObject<List<LanguageLevel>>(apiResponse);
                }
            }
            return _oLanguageLevels;
        }

        [HttpGet("{Id}")]
        public async Task<LanguageLevel> GetLanguageLevel(int Id)
        {
            _oLanguageLevel = new LanguageLevel();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/LanguageLevels/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oLanguageLevel = JsonConvert.DeserializeObject<LanguageLevel>(apiResponse);
                }
            }
            return _oLanguageLevel;
        }

        [HttpPost]
        public async Task<LanguageLevel> PostLanguageLevel(LanguageLevel languageLevel)
        {
            _oLanguageLevel = new LanguageLevel();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(languageLevel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/LanguageLevels", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oLanguageLevel = JsonConvert.DeserializeObject<LanguageLevel>(apiResponse);
                }
            }
            return _oLanguageLevel;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutLanguageLevel(int Id, LanguageLevel languageLevel)
        {
            _oLanguageLevel = new LanguageLevel();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(languageLevel), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/LanguageLevels/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oLanguageLevel = JsonConvert.DeserializeObject<LanguageLevel>(apiResponse);
                        return Ok(_oLanguageLevel);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteLanguageLevel(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/LanguageLevels/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
