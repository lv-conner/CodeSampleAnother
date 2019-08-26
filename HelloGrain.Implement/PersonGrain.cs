using HelloGrain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace HelloGrain.Implement
{
    public class PersonGrain : Orleans.Grain<List<Person>>, IPersonGrain
    {
        public Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return Task.FromResult(State.AsEnumerable()); 
        }

        public Task<Person> GetPersonAsync(string id)
        {
            var person = State.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(person);
        }
        protected override Task ReadStateAsync()
        {
            State = new List<Person>()
            {
                new Person()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "tim lv"
                },
                new Person()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "person1"
                }
            };
            return Task.CompletedTask;
        }
    }
}
