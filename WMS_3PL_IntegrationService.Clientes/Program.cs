using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using WMS_3PL_IntegrationService.Clientes.Jobs;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace WMS_3PL_IntegrationService.Clientes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args)
                     .ConfigureServices((hostContext, services) =>
                     {
                         var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                         IConfiguration configuration = builder.Build();

                         string intervalo = (configuration["IntervaloServicio"]);
                         services.AddQuartz(q =>
                         {
                             q.UseMicrosoftDependencyInjectionScopedJobFactory();


                             var jobKey = new JobKey("ClientesJob");


                             q.AddJob<ClientesJob>(opts => opts.WithIdentity(jobKey));

                             // Create a trigger for the job
                             q.AddTrigger(opts => opts
                                .ForJob(jobKey)
                                .WithIdentity("ClientesJob-trigger") // give the trigger a unique name
                                 .WithCronSchedule(intervalo));
                         });
                         services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                     }).UseWindowsService().ConfigureServices((hostContext, services) => {

                     });
    }
}
