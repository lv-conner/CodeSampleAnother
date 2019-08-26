using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeSampleWebApp.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public List<string> Roles { get; set; }
    }
}
