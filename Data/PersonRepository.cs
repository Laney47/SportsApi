using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SportsApi.Data
{
    public class PersonRepository : DocumentDb
    {
        //each repo can specify it's own database and document collection
        public PersonRepository() : base("TestDb", "People") { }

        public Task<List<ToDoItem>> GetPeopleAsync()
        {
            return Task<List<ToDoItem>>.Run(() =>
                Client.CreateDocumentQuery<ToDoItem>(Collection.DocumentsLink)
                .ToList());
        }

        public Task<ToDoItem> GetPersonAsync(string id)
        {
            return Task<ToDoItem>.Run(() =>
                Client.CreateDocumentQuery<ToDoItem>(Collection.DocumentsLink)
                .Where(p => p.ID == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        public Task<ResourceResponse<Document>> CreatePerson(ToDoItem person)
        {
            return Client.CreateDocumentAsync(Collection.DocumentsLink, person);
        }

        public Task<ResourceResponse<Document>> UpdatePersonAsync(ToDoItem person)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == person.ID)
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, person);
        }

        public Task<ResourceResponse<Document>> DeletePersonAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }

        public Task<List<ToDoItem>> GetPeopleByLastNameAsync(string lastName)
        {
            return Task.Run(() =>
                Client.CreateDocumentQuery<ToDoItem>(Collection.DocumentsLink)
                .Where(p => p.Owner == lastName)
                .ToList());
        }
    }
}