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
    public class StatsController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        public StatsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<Stat> GetStat()
        {
            Stat _oStat = new Stat();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Stats"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oStat = JsonConvert.DeserializeObject<Stat>(apiResponse);
                }
            }
            return _oStat;
        }
    }
}
