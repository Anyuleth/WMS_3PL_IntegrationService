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
                var enviadoSFTP = false;
                var mensaje = string.Empty;
                ENTITY.Clientes clientes = new ENTITY.Clientes();
                clientes.Lista_Clientes = DAL.Clientes.Clientes.ObtenerClientesPendientes();
                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.Clientes));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Clientes.xml", clientes, serialiser);

                UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\", "Clientes.xml", out enviadoSFTP,out mensaje);

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: SendData - Metodo: SendWMS_3PLClientes", mensaje.ToString()));

            }


        }

    }
}