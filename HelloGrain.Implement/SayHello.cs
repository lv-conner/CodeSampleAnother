using HelloGrain.Interface;
using System;
using System.Threading.Tasks;

namespace HelloGrain.Implement
{
    public class SayHello : Orleans.Grain, ISayHello
    {
        public Task<string> Hello(string name)
        {
            return Task.FromResult("Hello" + name);
        }
    }
}
