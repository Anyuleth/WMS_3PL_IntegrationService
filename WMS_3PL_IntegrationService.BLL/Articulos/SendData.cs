using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.BLL.Articulos
{
    public class SendData
    {
        public static void SendWMS_3PLArticulos()
        {
            try
            {
                ENTITY.Articulos articulos = new ENTITY.Articulos();
                articulos.Lista_articulos = DAL.Articulos.Articulos.ObtenerArticulosPendientes();

                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.Articulos));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Articulos.xml", articulos, serialiser);

                UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\", "Articulos.xml");

            }
            catch (Exception ex)
            {


            }


        }
       
    }
}
