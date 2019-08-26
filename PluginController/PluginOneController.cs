using Microsoft.AspNetCore.Mvc;
using System;

namespace PluginController
{
    public class PluginOneController:Controller
    {
        public string Index()
        {
            return "PluginOneController";
        } 
        public IActionResult Hello()
        {
            return View();
        }
    }
}
