using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
namespace WMS_3PL_IntegrationService.ConfirmacionPedidoCompra.Jobs
{
   
    [DisallowConcurrentExecution]
    public class ConfirmacionPedidoCompraJob : IJob
    {
        private readonly ILogger<ConfirmacionPedidoCompraJob> _logger;
        public ConfirmacionPedidoCompraJob(ILogger<ConfirmacionPedidoCompraJob> logger)
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
