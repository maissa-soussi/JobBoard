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
    public class DiplomaDTOController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        DiplomaDTO _oDiploma = new DiplomaDTO();

        public DiplomaDTOController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<DiplomaDTO> GetDiplomaDTO(int id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/DiplomaDTO/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDiploma = JsonConvert.DeserializeObject<DiplomaDTO>(apiResponse);
                }
            }
            return _oDiploma;
        }
    }
}
