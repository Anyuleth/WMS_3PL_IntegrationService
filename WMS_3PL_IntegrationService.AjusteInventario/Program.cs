using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using WMS_3PL_IntegrationService.AjusteInventario.Jobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System;

namespace WMS_3PL_IntegrationService.AjusteInventario
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
                             Dictionary<string, string> parameters = new Dictionary<string, string>();
                             parameters.Add("args", args[1].ToString());

                             var jobKey = new JobKey("AjusteInventarioJob");


                             q.AddJob<AjusteInventarioJob>(opts => opts.WithIdentity(jobKey));

                             // Create a trigger for the job
                             q.AddTrigger(opts => opts
                                .ForJob(jobKey)
                                .WithIdentity("AjusteInventarioJob-trigger") // give the trigger a unique name
                                 .WithCronSchedule(intervalo).UsingJobData(new JobDataMap(parameters)));
                         });
                         services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                     }).UseWindowsService().ConfigureServices((hostContext, services) => {

                     });
    }
}
