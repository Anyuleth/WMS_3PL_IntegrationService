using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using WMS_3PL_IntegrationService.PedidosCompras.Jobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Configuration;
using System;

namespace WMS_3PL_IntegrationService.PedidosCompras
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parameters = Environment.GetCommandLineArgs();
            try
            {

                CreateHostBuilder(parameters).Build().Run();
            }
            catch (Exception ex)
            {
                UTILITY.Files.LogException(ex, "Main" + parameters);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args)
                     .ConfigureServices((hostContext, services) =>
                     {
                         string intervalo = ConfigurationManager.AppSettings["IntervaloServicio"].ToString();
                         services.AddQuartz(q =>
                         {
                             q.UseMicrosoftDependencyInjectionScopedJobFactory();


                             var jobKey = new JobKey("PedidosComprasJob");


                             q.AddJob<PedidosComprasJob>(opts => opts.WithIdentity(jobKey));

                             // Create a trigger for the job
                             q.AddTrigger(opts => opts
                                .ForJob(jobKey)
                                .WithIdentity("PedidosComprasJob-trigger") // give the trigger a unique name
                                 .WithCronSchedule(intervalo));
                         });
                         services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                     }).UseWindowsService().ConfigureServices((hostContext, services) => {

                     });
    }
}
