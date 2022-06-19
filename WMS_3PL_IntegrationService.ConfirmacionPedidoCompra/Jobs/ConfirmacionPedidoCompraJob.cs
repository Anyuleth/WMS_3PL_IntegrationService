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

            var dataMap = context.MergedJobDataMap;
            var args = (string[])dataMap["args"];

            string servidorBD = "3.224.17.235, 14333";//args[1];
            string nombreBD = "MULTIBRANDS_GAP";//args[2];
            string usuarioBD = "icgadmin";//args[3];
            string contrasennaBD = "masterkey";//args[4];

            BLL.ConfirmacionPedidoCompra.SendData.CheckWMS_3PLPedidos(servidorBD, nombreBD, usuarioBD, contrasennaBD);

            return Task.CompletedTask;
        }
    }
}
