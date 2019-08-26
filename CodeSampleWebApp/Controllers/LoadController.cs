using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CodeSampleWebApp.CustomerProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace CodeSampleWebApp.Controllers
{
    public class LoadController : Controller
    {

        private readonly ApplicationPartManager _applicationPartManager;
        private readonly PluginActionDescriptorProvider _pluginActionDescriptorProvider;
        public LoadController(ApplicationPartManager applicationPartManager, PluginActionDescriptorProvider pluginActionDescriptorProvider)
        {
            _applicationPartManager = applicationPartManager;
            _pluginActionDescriptorProvider = pluginActionDescriptorProvider;
        }
        public IActionResult Index()
        {
            var assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "DLL\\PluginController\\PluginController.dll");
            var viewAssembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "DLL\\PluginController\\PluginController.Views.dll");

            var controllerAssemblyPart = new AssemblyPart(assembly);
            var viewAssemblyPart = new CompiledRazorAssemblyPart(viewAssembly);
            _applicationPartManager.ApplicationParts.Add(controllerAssemblyPart);
            _applicationPartManager.ApplicationParts.Add(viewAssemblyPart);

            _pluginActionDescriptorProvider.Reload();
            var feature = new ViewsFeature();
            _applicationPartManager.PopulateFeature<ViewsFeature>(feature);
            return Content("Enabled"); ;
        }
    }
}