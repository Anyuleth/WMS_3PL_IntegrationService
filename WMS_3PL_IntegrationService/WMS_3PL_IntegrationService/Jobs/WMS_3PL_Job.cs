using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace WMS_3PL_IntegrationService.Jobs
{
    [DisallowConcurrentExecution]
    public class WMS_3PL_Job : IJob
    {
        private readonly ILogger<WMS_3PL_Job> _logger;
        public WMS_3PL_Job(ILogger<WMS_3PL_Job> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");
            BLL.SendData.SendWMS_3PLReports();

            return Task.CompletedTask;
        }
    }
}
