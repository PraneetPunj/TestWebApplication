using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestWebApplication.Contracts;
using TestWebApplication.Models;

namespace TestWebApplication.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public PersonRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        async Task<IEnumerable<Person>> IPersonRepository.GetAll()
        {
            var _client = _clientFactory.CreateClient("datarequest");
            _client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await _client.GetAsync("/people.json");
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            JArray people = JArray.Parse(result);
            return people.ToObject<List<Person>>();

        }
    }
}
