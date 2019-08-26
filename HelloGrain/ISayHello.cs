using System;
using System.Threading.Tasks;

namespace HelloGrain.Interface
{
    public interface ISayHello: Orleans.IGrainWithIntegerKey
    {
        Task<string> Hello(string name); 
    }
}
