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
    public class DomainDTOController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        DomainDTO _oDomain = new DomainDTO();
  
        public DomainDTOController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<DomainDTO> GetDomainDTO(int id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/DomainDTO/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oDomain= JsonConvert.DeserializeObject<DomainDTO>(apiResponse);
                }
            }
            return _oDomain;
        }

    }
}
