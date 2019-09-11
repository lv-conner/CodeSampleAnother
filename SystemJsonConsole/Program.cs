using System;
using System.Text.Json;

namespace SystemJsonConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "tim lv"
            };
            var s = JsonSerializer.Serialize(p);
            var p1 = JsonSerializer.Deserialize<Person>(s);
            Console.WriteLine(p1.Name);
            Console.WriteLine("Hello World!");
        }
    }
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
