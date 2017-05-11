using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.Diagnostics;

using System.Configuration;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SportsApi.Data;

namespace SportsApi.Controllers
{
    public class ToDoListController : ApiController
    {

        private PersonRepository _repo;

        public ToDoListController()
        {
            Initilization = InitializeAsync();
        }

        public Task Initilization { get; private set; }

        private async Task InitializeAsync()
        {
            _repo = new PersonRepository();
            await _repo.Initilization;
        }

        // GET: api/ToDoItemList
        public async Task<IHttpActionResult> Get()
        {
            await Initilization;
            var people = await _repo.GetPeopleAsync();
            return Ok(people);
        }

        // GET: api/ToDoItem/5
        public async Task<IHttpActionResult> Get(string id)
        {
            await Initilization;
            var person = await _repo.GetPersonAsync(id);
            if (person != null)
                return Ok(person);
            return NotFound();
        }



        // POST: api/ToDoItem
        public async Task<IHttpActionResult> Post(ToDoItem person)
        {
            await Initilization;
            var response = await _repo.CreatePerson(person);
            return Ok(response.Resource);
        }

        // PUT: api/ToDoItem
        public async Task<IHttpActionResult> Put(ToDoItem person)
        {
            await Initilization;
            var response = await _repo.UpdatePersonAsync(person);
            return Ok(response.Resource);
        }

        // Delete: api/ToDoItem
        public async Task<IHttpActionResult> Delete(string id)
        {
            await Initilization;
            var response = await _repo.DeletePersonAsync(id);
            return Ok();
        }
        
    }
}

