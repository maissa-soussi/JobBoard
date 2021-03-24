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
    public class UsersController : ControllerBase
    {
        HttpClientHandler _clientHandler = new HttpClientHandler();

        User _oUser = new User();
        List<User> _oUsers = new List<User>();
        public UsersController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        [HttpGet]
        public async Task<List<User>> GetUser()
        {
            _oUsers = new List<User>();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Users"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oUsers = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            return _oUsers;
        }

        [HttpGet("{Id}")]
        public async Task<User> GetUser(int Id)
        {
            _oUser = new User();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.GetAsync("https://localhost:44304/api/Users/" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oUser = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return _oUser;
        }

        [HttpPost]
        public async Task<User> PostUser(User user)
        {
            _oUser = new User();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44304/api/Users", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _oUser = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return _oUser;
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutUser(int Id, User user)
        {
            _oUser = new User();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("https://localhost:44304/api/Users/" + Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        _oUser = JsonConvert.DeserializeObject<User>(apiResponse);
                        return Ok(_oUser);
                    }
                    else
                        return BadRequest();
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<string> DeleteUser(int Id)
        {
            string message = "";
            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:44304/api/Users/" + Id))
                {
                    message = await response.Content.ReadAsStringAsync();
                }
            }
            return message;
        }
    }
}
