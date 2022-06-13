using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace WMS_3PL_IntegrationService.Articulos.Jobs
{
    
    [DisallowConcurrentExecution]
    public class ArticulosJob : IJob
    {
        private readonly ILogger<ArticulosJob> _logger;
        public ArticulosJob(ILogger<ArticulosJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");
            BLL.Articulos.SendData.SendWMS_3PLArticulos();
            return Task.CompletedTask;
        }
    }
}
