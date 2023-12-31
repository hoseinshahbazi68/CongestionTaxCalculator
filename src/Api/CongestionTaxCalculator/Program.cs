﻿using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using TebCongestionTaxCalculator;

namespace CongestionTaxCalculator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Set default proxy
            //WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                var webHost = CreateHostBuilder(args).Build();

                using var scope = webHost.Services.CreateScope();

                await webHost.RunAsync();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(options => options.ClearProviders())
                .ConfigureLogging(logger =>
                {
                    logger.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                    logger.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Information);
                    logger.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
                    logger.AddFilter(DbLoggerCategory.Update.Name, LogLevel.Information);
                    logger.ClearProviders();
                    logger.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        //use in cmd mode, not iis express
                        //.UseKestrel(c =>
                        //{
                        //    c.AddServerHeader = false;
                        //    c.Limits.MaxResponseBufferSize = 52428800; //50MB
                        //})
                        //.UseIISIntegration()
                        .UseStartup<Startup>();
                });
    }
}
