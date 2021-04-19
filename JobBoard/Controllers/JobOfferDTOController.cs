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
    public class JobOfferDtoController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        List<JobOfferDTO> _jobOfferDTOs= new List<JobOfferDTO>();

        //User _oUser = new User();
        public JobOfferDtoController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        [HttpGet]
        public async Task<List<JobOfferDTO>> GetJobOfferDTO()
        {
            _jobOfferDTOs = new List<JobOfferDTO>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/JobOfferDTO"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _jobOfferDTOs = JsonConvert.DeserializeObject<List<JobOfferDTO>>(apiResponse);
                }
            }
            return _jobOfferDTOs;
        }

    }
}
