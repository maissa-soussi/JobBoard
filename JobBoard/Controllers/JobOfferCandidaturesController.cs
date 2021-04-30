using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JobBoard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobBoard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobOfferCandidaturesController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        JobOfferCandidature _oJobOfferCandidature = new JobOfferCandidature();
        List<JobOfferCandidature> _oJobOfferCandidatures = new List<JobOfferCandidature>();
        public JobOfferCandidaturesController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{Id}")]
        public async Task<List<JobOfferCandidature>> GetJobOfferCandidature(int Id)
        {
            _oJobOfferCandidatures = new List<JobOfferCandidature>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/JobOfferCandidatures/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oJobOfferCandidatures = JsonConvert.DeserializeObject<List<JobOfferCandidature>>(apiResponse);
                }
            }
            return _oJobOfferCandidatures;
        }

        
    }
}
