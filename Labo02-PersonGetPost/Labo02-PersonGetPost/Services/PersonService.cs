using Labo02_PersonGetPost.Models;
using Microsoft.VisualBasic;
using System.Runtime.ExceptionServices;

namespace Labo02_PersonGetPost.Services
{
    public class PersonService
    {
        private static readonly List<Person> AllPersons = new();

        public Task CreatePerson(Person item)
        {
            AllPersons.Add(item);
            return Task.CompletedTask;
        }

        public Task<Person?> GetPerson(int id)
        {
            return Task.FromResult(AllPersons.FirstOrDefault(x => x.Id == id));
        }

        public Task<List<Person>> GetAllPersons()
        {
            return Task.FromResult(AllPersons);
        }

        public Task<Person?> UpdatePerson(int id, Person item)
        {
            var person = AllPersons.FirstOrDefault(x => x.Id == id);
            if (person != null)
            {
                person.Id = item.Id;
                person.FirstName = item.FirstName;
                person.LastName = item.LastName;
                person.Address = item.Address;
            }

            return Task.FromResult(person);
        }

        public Task DeletePerson(int id)
        {
            var person = AllPersons.FirstOrDefault(x => x.Id == id);
            if (person != null)
            {
                AllPersons.Remove(person);
            }

            return Task.CompletedTask;
        }
    }
}
