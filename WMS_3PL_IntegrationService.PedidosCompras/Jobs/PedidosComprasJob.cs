using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
namespace WMS_3PL_IntegrationService.PedidosCompras.Jobs
{
    
    [DisallowConcurrentExecution]
    public class PedidosComprasJob : IJob
    {
        private readonly ILogger<PedidosComprasJob> _logger;
        public PedidosComprasJob(ILogger<PedidosComprasJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");
            BLL.PedidosCompras.SendData.SendWMS_3PLPedidos();

            return Task.CompletedTask;
        }
    }
}
