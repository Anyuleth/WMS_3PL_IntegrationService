using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace WMS_3PL_IntegrationService.AjusteInventario.Jobs
{
   
    [DisallowConcurrentExecution]
    public class AjusteInventarioJob : IJob
    {
        private readonly ILogger<AjusteInventarioJob> _logger;
        public AjusteInventarioJob(ILogger<AjusteInventarioJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");
            //BLL.SendData.SendWMS_3PLReports();

            return Task.CompletedTask;
        }
    }
}
