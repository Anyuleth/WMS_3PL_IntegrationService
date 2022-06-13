using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace WMS_3PL_IntegrationService.CodigoBarras.Jobs
{
   
    [DisallowConcurrentExecution]
    public class CodigoBarrasJob : IJob
    {
        private readonly ILogger<CodigoBarrasJob> _logger;
        public CodigoBarrasJob(ILogger<CodigoBarrasJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");
            BLL.CodigoBarras.SendData.SendWMS_3PLCodigoBarras();

            return Task.CompletedTask;
        }
    }
}
