using System;
using System.Collections.Generic;
using System.Text;

namespace WMS_3PL_IntegrationService.PedidosCompras
{
    public class AppSettings
    {
        public string IntervaloServicio { get; set; }
        public string ConnectionString { get; set; }
        public string HostSFTP{ get; set; }
        public string Puerto{ get; set; }
        public string Usuario{ get; set; }
        public string pass{ get; set; }
        public string FromWMS{ get; set; }
        public string ToWMS{ get; set; }
        public string CarpetaPedidos{ get; set; }
        public string NombreArchivoXML{ get; set; }
        public string ExtencionArchivoXML{ get; set; }
    }
}
