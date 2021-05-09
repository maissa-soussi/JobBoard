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
    [Route("api/[controller]")]
    [ApiController]
    public class EducationLevelDTOController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        EducationLevelDTO _oEducationLevel = new EducationLevelDTO();

        public EducationLevelDTOController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<EducationLevelDTO> GetEducationLevelDTO(int id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/EducationLevelDTO/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oEducationLevel = JsonConvert.DeserializeObject<EducationLevelDTO>(apiResponse);
                }
            }
            return _oEducationLevel;
        }
    }
}
