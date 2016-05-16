using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHome.Host.Self
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Starting server...");
            var server = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:8282"));
            server.Start();
            Console.WriteLine("started!");
            Console.WriteLine("press any key to exit");
            Console.Read();
        }
    }
}
