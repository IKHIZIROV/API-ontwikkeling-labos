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
    }
}
