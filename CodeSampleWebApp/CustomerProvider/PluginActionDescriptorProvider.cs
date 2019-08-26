using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeSampleWebApp.CustomerProvider
{
    public class PluginActionDescriptorProvider : IActionDescriptorChangeProvider
    {
        private CancellationTokenSource _tokenSource;
        private CancellationChangeToken _token;
        public IChangeToken GetChangeToken()
        {
            _tokenSource = new CancellationTokenSource();
            _token = new CancellationChangeToken(_tokenSource.Token);
            return _token;
        }
        public void Reload()
        {
            _tokenSource.Cancel();
        }
    }
}
