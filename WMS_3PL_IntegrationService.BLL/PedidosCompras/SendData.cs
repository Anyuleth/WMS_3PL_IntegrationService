using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.BLL.PedidosCompras
{
    public class SendData
    {
        public static void SendWMS_3PLPedidos()
        {
            try
            {
                List<ENTITY.PedidosCompras.PedidosCompra> pedidos = new List<ENTITY.PedidosCompras.PedidosCompra>();
                pedidos = DAL.PedidosCompras.PedidosCompras.ObtenerPedidosPendientes();
                

                foreach (var item in pedidos)
                {
                
                    item.Linea = DAL.PedidosCompras.PedidosCompras.ObtenerPedidosDetallePendientes();
                  

                  
                }
                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.PedidosCompras.PedidosCompra));
                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\PedidosCompras.xml", pedidos, serialiser);

                //UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\PedidosCompras.xml");



            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: ProcessData - Metodo: SendWMS_3PLPedidos", mensaje.ToString()));

            }


        }
       
    }
}
