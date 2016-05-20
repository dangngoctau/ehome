using EHome.Infrastructure;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Api.Modules
{
    public class HomeModule : NancyModule
    {
       // private readonly IGateway _gateway;
        public HomeModule(IGateway gateway)
        {

           // _gateway = gateway;

            Get["/"] = _ =>
            {
               // _gateway.Start();
                return "Hello";
            };
        }
    }
}
