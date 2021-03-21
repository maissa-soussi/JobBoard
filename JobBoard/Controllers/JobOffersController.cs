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
    public class JobOffersController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        JobOffer _oJobOffer = new JobOffer();
        List<JobOffer> _oJobOffers = new List<JobOffer>();
        public JobOffersController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<JobOffer>> GetJobOffer()
        {
            _oJobOffers = new List<JobOffer>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/JobOffers"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oJobOffers = JsonConvert.DeserializeObject<List<JobOffer>>(apiResponse);
                }
            }
            return _oJobOffers;
        }

        [HttpGet("{Id}")]
        public async Task<JobOffer> GetJobOffer(int Id)
        {
            _oJobOffer = new JobOffer();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/JobOffers/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oJobOffer = JsonConvert.DeserializeObject<JobOffer>(apiResponse);
                }
            }
            return _oJobOffer;
        }

        [HttpPost]
        public async Task<JobOffer> PostContratType(JobOffer jobOffer)
        {
            _oJobOffer = new JobOffer();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(jobOffer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/JobOffers", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oJobOffer = JsonConvert.DeserializeObject<JobOffer>(apiResponse);
                }
            }
            return _oJobOffer;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutJobOffer(int Id, JobOffer jobOffer)
        {
            _oJobOffer = new JobOffer();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(jobOffer), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/JobOffers/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oJobOffer = JsonConvert.DeserializeObject<JobOffer>(apiResponse);
                        return Ok(_oJobOffer);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteJobOffer(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/JobOffers/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
