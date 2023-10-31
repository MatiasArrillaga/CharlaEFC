using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Net;

namespace Algoritmo.CharlaEFC.API
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);



        /// <summary>
        /// Ejecución principal.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();
            Log.Logger = CreateSerilogLogger(configuration);



            try
            {
                Log.Information("Configurando web host ({ApplicationContext})...", AppName);
                var host = GetHostBuilder(configuration, args).Build();



                Log.Information("Iniciando web host ({ApplicationContext})...", AppName);
                host.Run();



                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Programa terminado inesperadamente ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.Information("Finalizando web host ({ApplicationContext})...", AppName);
                Log.CloseAndFlush();
            }
        }



        /// <summary>
        /// Retorna el WebHostBuilder.
        /// Se utiliza para las invocaciones externas como los proyectos de testing que necesitan levantar una instancia del servidor configurada.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return GetHostBuilder(GetConfiguration(), args);
        }



        /// <summary>
        /// Crea el WebHostBuilder y canaliza el logging si corresponde.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IWebHostBuilder GetHostBuilder(IConfiguration configuration, string[] args)
        {
            var hostBuilder = WebHost.CreateDefaultBuilder(args)
            .UseConfiguration(configuration)
            .CaptureStartupErrors(false)
            .ConfigureKestrel(options =>
            {
                var httpPort = configuration.GetValue("PORT", 80);
                options.Listen(IPAddress.Any, httpPort, listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                });



            })
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseSerilog((hostingContext, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(configuration));



            return hostBuilder;
        }



        /// <summary>
        /// recupera la configuración desde los archivos json y fuerza la recarga ante cambios.
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
            .AddEnvironmentVariables();



            var config = builder.Build();



            if (config.GetValue<bool>("UseVault", false))
            {
                // Punto de extensión para completar con credenciales de Azure.
            }



            return builder.Build();
        }



        /// <summary>
        /// Crea y configura la instancia del logger.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var logstashUrl = configuration["Serilog:LogstashUrl"];
            return new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.WithProperty("ApplicationContext", AppName)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
            .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }



        #region Implementación default DEPRECADA
        //public static void Main(string[] args)
        //{
        // CreateHostBuilder(args).Build().Run();



        //}



        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        // Host.CreateDefaultBuilder(args)
        // .ConfigureWebHostDefaults(webBuilder =>
        // {
        // webBuilder.UseStartup<Startup>();
        // })
        // .UseSerilog((hostingContext, loggerConfig) =>
        // loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
        // );
        #endregion
    }
}
