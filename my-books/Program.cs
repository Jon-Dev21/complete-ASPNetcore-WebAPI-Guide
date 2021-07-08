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
                // Creating configuration builder. read from AppSettings.json logger section
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                // CREATING LOGGER FOR SERILOG
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                // Old logger before configuring appsettings.json
                //Log.Logger = new LoggerConfiguration()
                //    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // To write log to file. (rollingInterval: RollingInterval.Day means that a log will be created per day)
                //    .CreateLogger();

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
