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
    public class CountryDTOController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        CountryDTO _oCountry = new CountryDTO();

        public CountryDTOController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet("{id}")]
        public async Task<CountryDTO> GetCountryDTO(int id)
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/CountryDTO/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oCountry = JsonConvert.DeserializeObject<CountryDTO>(apiResponse);
                }
            }
            return _oCountry;
        }
    }
}
