using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace consoleApi
{
    class Program
    {

        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:8080"))
            {
                Console.WriteLine("WebServer is running");
                Console.WriteLine("Press any key to quit");
                Console.ReadLine();
            }
        }
    }
}
