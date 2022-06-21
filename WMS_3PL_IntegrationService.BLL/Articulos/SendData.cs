using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.BLL.Articulos
{
    public class SendData
    {
        #region Enviar los articulos al SFTP
        public static void SendWMS_3PLArticulos()
        {
            try
            {
                var enviadoSFTP = false;
                var mensaje = string.Empty;
                ENTITY.Articulos articulos = new ENTITY.Articulos();
                articulos.Lista_articulos = DAL.Articulos.Articulos.ObtenerArticulosPendientes();

                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.Articulos));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Articulos.xml", articulos, serialiser);

                UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\", "Articulos.xml", out enviadoSFTP, out mensaje);

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: SendData - Metodo: SendWMS_3PLArticulos", mensaje.ToString()));

            }


        }
        #endregion


    }
}
