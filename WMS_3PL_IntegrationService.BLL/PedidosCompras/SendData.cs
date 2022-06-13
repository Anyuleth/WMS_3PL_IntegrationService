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
                List < ENTITY.PedidosCompras.PedidosCompra> pedidos = new List<ENTITY.PedidosCompras.PedidosCompra>();
                pedidos = DAL.PedidosCompras.PedidosCompras.ObtenerPedidosPendientes();

                foreach (var item in pedidos)
                {
                    XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.PedidosCompras.PedidosCompras));

                    UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\PedidosCompras.xml", pedidos, serialiser);

                    //UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\PedidosCompras.xml");
                }



            }
            catch (Exception ex)
            {


            }


        }
       
    }
}
