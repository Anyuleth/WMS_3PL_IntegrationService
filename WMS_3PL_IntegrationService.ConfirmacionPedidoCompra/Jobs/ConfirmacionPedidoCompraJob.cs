using Microsoft.Extensions.Logging;
using Quartz;
using System.Configuration;
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

            string servidorBD = ConfigurationManager.AppSettings["Server"].ToString(); //"3.224.17.235,14333";//args[0];//
            string nombreBD = args[0];//"MULTIBRANDS_GAP";//
            string usuarioBD = ConfigurationManager.AppSettings["User"].ToString();// "icgadmin";//args[2];//
            string contrasennaBD = ConfigurationManager.AppSettings["Password"].ToString();// "masterkey";//args[3];//

            BLL.ConfirmacionPedidoCompra.SendData.CheckWMS_3PLPedidos(servidorBD, nombreBD, usuarioBD, contrasennaBD);

            return Task.CompletedTask;
        }
    }
}
