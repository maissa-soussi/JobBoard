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
    public class AuthController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();
        User _oUser = new User();
        public AuthController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpPost]
        public async Task<User> PostUser(User user)
        {
            _oUser = new User();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/auth/login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oUser = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return _oUser;
        }
    }
}
