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
    public class SalaryWishesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        SalaryWish _oSalaryWish = new SalaryWish();
        List<SalaryWish> _oSalaryWishes = new List<SalaryWish>();
        public SalaryWishesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<SalaryWish>> GetSalaryWish()
        {
            _oSalaryWishes = new List<SalaryWish>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/SalaryWishes"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSalaryWishes = JsonConvert.DeserializeObject<List<SalaryWish>>(apiResponse);
                }
            }
            return _oSalaryWishes;
        }

        [HttpGet("{Id}")]
        public async Task<SalaryWish> GetSalaryWish(int Id)
        {
            _oSalaryWish = new SalaryWish();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/SalaryWishes/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSalaryWish = JsonConvert.DeserializeObject<SalaryWish>(apiResponse);
                }
            }
            return _oSalaryWish;
        }

        [HttpPost]
        public async Task<SalaryWish> PostSalaryWish(SalaryWish salaryWish)
        {
            _oSalaryWish = new SalaryWish();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(salaryWish), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/SalaryWishes", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oSalaryWish = JsonConvert.DeserializeObject<SalaryWish>(apiResponse);
                }
            }
            return _oSalaryWish;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutSalaryWish(int Id, SalaryWish salaryWish)
        {
            _oSalaryWish = new SalaryWish();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(salaryWish), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/SalaryWishes/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oSalaryWish = JsonConvert.DeserializeObject<SalaryWish>(apiResponse);
                        return Ok(_oSalaryWish);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteSalaryWish(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/SalaryWishes/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
