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
    public class TriController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        DiplomaDTO _oDiploma = new DiplomaDTO();
        List<int >_idCandidats =new List<int>();
        List<Candidature> liste = new List<Candidature>();

        public TriController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<List<Candidature>> GetDiplomaDTO(int id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/predict/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    liste = JsonConvert.DeserializeObject<List<Candidature>>(apiResponse);
                }
            }
        
            return liste;
        }
    }
}
