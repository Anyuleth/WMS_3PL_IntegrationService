using Microsoft.Extensions.Logging;
using Quartz;
using System;
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
            var dataMap = context.MergedJobDataMap;
            var args = (string)dataMap["args"];
            try
            {
                _logger.LogInformation("WMS_3PL_Job Executing!");

              
            

                string servidorBD = ConfigurationManager.AppSettings["Server"].ToString(); 
                string nombreBD = args;
                string usuarioBD = ConfigurationManager.AppSettings["User"].ToString();
                string contrasennaBD = ConfigurationManager.AppSettings["Password"].ToString();

                BLL.ConfirmacionPedidoCompra.SendData.CheckWMS_3PLPedidos(servidorBD, nombreBD, usuarioBD, contrasennaBD);
                UTILITY.Files.LogInformation(args, "Execute Job");

            }
            catch (Exception ex )
            {

                _logger.LogError(ex.InnerException != null ? ex.InnerException.Message + args[0].ToString() : ex.Message + args[0].ToString());
            }
            return Task.CompletedTask;
        }
    }
}
