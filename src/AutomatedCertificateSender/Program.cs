using System;
using Microsoft.Extensions.Hosting;

namespace automated_certificate_sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => {

                    
                });
    }
}
