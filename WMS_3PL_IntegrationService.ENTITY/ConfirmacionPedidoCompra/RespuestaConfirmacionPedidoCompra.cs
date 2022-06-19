using System;
using System.Collections.Generic;
using System.Text;

namespace WMS_3PL_IntegrationService.ENTITY.ConfirmacionPedidoCompra
{
    public class RespuestaConfirmacionPedidoCompra
    {
        
        //public string Linea_Numero { get; set; }
        //public string Empresa { get; set; }
        //public string Documento { get; set; }
        //public string Tipo { get; set; }
        //public string Producto { get; set; }
        public string JsonDiferencias { get; set; }
        public bool Diferencias { get; set; }
    }
}
