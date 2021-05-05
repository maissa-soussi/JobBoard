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
    public class AdminsController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        User _oUser = new User();
        List<User> _oAdmins = new List<User>();
        public AdminsController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        [HttpGet]
        public async Task<List<User>> GetUser()
        {
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Admins"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oAdmins = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            return _oAdmins;
        }
    }
}
