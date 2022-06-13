using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace WMS_3PL_IntegrationService.Precios.Jobs

{
    [DisallowConcurrentExecution]
    public class PreciosJob : IJob
    {
        private readonly ILogger<PreciosJob> _logger;
        public PreciosJob(ILogger<PreciosJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");
            BLL.Precios.SendData.SendWMS_3PLPrecios();

            return Task.CompletedTask;
        }
    }
}
