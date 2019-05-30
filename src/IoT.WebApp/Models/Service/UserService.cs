using IoT.WebApp.Models.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace IoT.WebApp.Models
{
    public class UserService
    {
        HttpClient _client = new HttpClient();

        public async Task<String> CreateAsync(User user)
        {
            String jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            var response = await _client.PostAsync("https://localhost:44383/api/v1/users/", new StringContent(jsonObject, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public IEntity Delete(int id)
        {
            return null;
            throw new NotImplementedException();
            
        }

        public IEntity Get(int id)
        {
            var resp = _client.GetAsync("https://localhost:44383/api/v1/users/" + id).Result;
            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<User>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public ICollection<User> GetAll()
        {
            var resp = _client.GetAsync("https://localhost:44383/api/v1/users").Result;
            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ICollection<User>>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public IEntity Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
