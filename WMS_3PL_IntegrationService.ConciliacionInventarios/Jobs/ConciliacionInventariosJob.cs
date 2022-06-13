using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace WMS_3PL_IntegrationService.ConciliacionInventarios.Jobs
{
   
    [DisallowConcurrentExecution]
    public class ConciliacionInventariosJob : IJob
    {
        private readonly ILogger<ConciliacionInventariosJob> _logger;
        public ConciliacionInventariosJob(ILogger<ConciliacionInventariosJob> logger)
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
