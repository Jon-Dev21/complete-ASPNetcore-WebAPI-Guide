using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Creating Logger for serilog
                Log.Logger = new LoggerConfiguration().CreateLogger();

                CreateHostBuilder(args).Build().Run();
            } 
            finally
            {
                // Resets logger to default and disposes original if possible
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()           // Instruction to enable use of serilog
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
