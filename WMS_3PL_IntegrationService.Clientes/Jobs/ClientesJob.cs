using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
namespace WMS_3PL_IntegrationService.Clientes.Jobs
{
    
    [DisallowConcurrentExecution]
    public class ClientesJob : IJob
    {
        private readonly ILogger<ClientesJob> _logger;
        public ClientesJob(ILogger<ClientesJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("WMS_3PL_Job Executing!");
            BLL.Clientes.SendData.SendWMS_3PLClientes();

            return Task.CompletedTask;
        }
    }
}
