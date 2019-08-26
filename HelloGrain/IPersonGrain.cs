using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloGrain.Interface
{
    public interface IPersonGrain:Orleans.IGrainWithIntegerKey
    {
        Task<Person> GetPersonAsync(string id);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
    }


    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
