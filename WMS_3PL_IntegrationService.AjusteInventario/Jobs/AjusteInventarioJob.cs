using Microsoft.Extensions.Logging;
using Quartz;
using System.Configuration;
using System.Threading.Tasks;

namespace WMS_3PL_IntegrationService.AjusteInventario.Jobs
{
   
    [DisallowConcurrentExecution]
    public class AjusteInventarioJob : IJob
    {
        private readonly ILogger<AjusteInventarioJob> _logger;
        public AjusteInventarioJob(ILogger<AjusteInventarioJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");

            var dataMap = context.MergedJobDataMap;
            var args = (string[])dataMap["args"];

            string servidorBD = ConfigurationManager.AppSettings["Server"].ToString(); //"3.224.17.235,14333";//args[0];//
            string nombreBD = "MULTIBRANDS_GAP";//args[0];
            string usuarioBD = ConfigurationManager.AppSettings["User"].ToString();// "icgadmin";//args[2];//
            string contrasennaBD = ConfigurationManager.AppSettings["Password"].ToString();// "masterkey";//args[3];//

            

            BLL.AjusteInventario.SendData.CheckWMS_3PLInventario(servidorBD, nombreBD, usuarioBD, contrasennaBD);

            return Task.CompletedTask;
        }
    }
}
