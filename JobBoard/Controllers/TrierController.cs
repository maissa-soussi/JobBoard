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
    public class TrierController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        List<Candidature> liste = new List<Candidature>();

        public TrierController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpPost]
        public async Task<List<Candidature>> PostTrier(Tri t)
        {
            
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Trier", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    liste = JsonConvert.DeserializeObject<List<Candidature>>(apiResponse);
                }
            }
            return liste;
        }
    }
}
