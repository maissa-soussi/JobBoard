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
    public class MyCandidatureSpontsController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        List<Candidature> _oCandidatures = new List<Candidature>();

        public MyCandidatureSpontsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<List<Candidature>> GetCandidature(int id)
        {
            _oCandidatures = new List<Candidature>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/MyCandidatureSponts/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidatures = JsonConvert.DeserializeObject<List<Candidature>>(apiResponse);
                }
            }
            return _oCandidatures;
        }
    }
}