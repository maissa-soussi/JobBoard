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
    public class LanguagesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        Language _oLanguage = new Language();
        List<Language> _oLanguages = new List<Language>();
        public LanguagesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<Language>> GetLanguage()
        {
            _oLanguages = new List<Language>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Languages"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oLanguages = JsonConvert.DeserializeObject<List<Language>>(apiResponse);
                }
            }
            return _oLanguages;
        }

        [HttpGet("{Id}")]
        public async Task<Language> GetLanguage(int Id)
        {
            _oLanguage = new Language();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Languages/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oLanguage = JsonConvert.DeserializeObject<Language>(apiResponse);
                }
            }
            return _oLanguage;
        }

        [HttpPost]
        public async Task<Language> PostLanguage(Language language)
        {
            _oLanguage = new Language();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(language), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Languages", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oLanguage = JsonConvert.DeserializeObject<Language>(apiResponse);
                }
            }
            return _oLanguage;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutLanguage(int Id, Language language)
        {
            _oLanguage = new Language();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(language), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Languages/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oLanguage = JsonConvert.DeserializeObject<Language>(apiResponse);
                        return Ok(_oLanguage);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteLanguage(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Languages/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
