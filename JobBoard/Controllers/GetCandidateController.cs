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
    public class GetCandidateController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        Candidate _oCandidate = new Candidate();

        public GetCandidateController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<Candidate> GetCandidate(int id)
        {
            _oCandidate = new Candidate();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/GetCandidate/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCandidate = JsonConvert.DeserializeObject<Candidate>(apiResponse);
                }
            }
            return _oCandidate;
        }
    }
}
