using Newtonsoft.Json;
using OrganizationInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OrganizationInfo.Services
{
    public class PrologMobileService : IPrologMobileService
    {
        private readonly string Baseurl = "https://5f0ddbee704cdf0016eaea16.mockapi.io/";
        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            List<Organization> orgs = new List<Organization>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await client.GetAsync("organizations");

                if (res.IsSuccessStatusCode)
                {
                    var ObjResponse = res.Content.ReadAsStringAsync().Result;
                    orgs = JsonConvert.DeserializeObject<List<Organization>>(ObjResponse);
                }
                return orgs;
            }
        }

        public async Task<IEnumerable<User>> GetOrgUsers(string id)
        {
            List<User> users = new List<User>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await client.GetAsync($"organizations/{id}/users");

                if (res.IsSuccessStatusCode)
                {
                    var ObjResponse = res.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<User>>(ObjResponse);
                }
                return users;
            }
        }
        public async Task<IEnumerable<Phone>> GetUserPhones(string orgId, string userId)
        {
            List<Phone> phones = new List<Phone>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await client.GetAsync($"organizations/{orgId}/users/{userId}/phones");

                if (res.IsSuccessStatusCode)
                {
                    var ObjResponse = res.Content.ReadAsStringAsync().Result;
                    phones = JsonConvert.DeserializeObject<List<Phone>>(ObjResponse);
                }
                return phones;
            }
        }

    }
}
