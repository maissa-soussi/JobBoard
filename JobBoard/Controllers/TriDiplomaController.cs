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
    public class TriDiplomaController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        List<int> _idCandidats = new List<int>();
        List<Candidature> liste = new List<Candidature>();

        public TriDiplomaController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<List<Candidature>> GetTriDiploma(int id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:4000/predict/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    liste = JsonConvert.DeserializeObject<List<Candidature>>(apiResponse);
                }
            }

            return liste;
        }
    }
}
