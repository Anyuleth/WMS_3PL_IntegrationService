using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.BLL.Clientes
{
    public class SendData
    {
        public static void SendWMS_3PLClientes()
        {
            try
            {
                ENTITY.Clientes clientes = new ENTITY.Clientes();
                clientes.Lista_Clientes = DAL.Clientes.Clientes.ObtenerClientesPendientes();
                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.Clientes));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Clientes.xml", clientes, serialiser);
              
                // UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\Articulos.xml");

            }
            catch (Exception ex)
            {


            }


        }

    }
}