using System;
using System.Collections.Generic;
using WMS_3PL_IntegrationService.UTILITY;
using WMS_3PL_IntegrationService.DAL.Articulos;
using System.Xml.Serialization;
using System.IO;

namespace WMS_3PL_IntegrationService.BLL
{
    public class SendData
    {
        public static void SendWMS_3PLReports()
        {
            try
            {
                ArticulosReport();
                //CodigoBarrasReport();
                //ClientesReport();

            }
            catch (Exception ex)
            {
                

            }


        }
        public static void ArticulosReport()
        {
            try
            {
                ENTITY.Articulos articulos = new ENTITY.Articulos();
                 articulos.Lista_articulos = DAL.Articulos.Articulos.ObtenerArticulosPendientes();

                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.Articulos));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Articulos.xml", articulos, serialiser);

               // UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\Articulos.xml");

            }
            catch (Exception ex)
            {
               
            }
        }

        public static void CodigoBarrasReport()
        {
            try
            {
                ENTITY.CodigoArticulos codigoArticulos = new ENTITY.CodigoArticulos();
                codigoArticulos.Lista_Codigos = DAL.Articulos.Articulos.ObtenerCodArticulosPendientes();

                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.CodigoArticulos));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Codigos de Barras.xml", codigoArticulos, serialiser);

            }
            catch (Exception ex)
            {

            }
        }

        public static void ClientesReport()
        {
            try
            {
                ENTITY.Clientes clientes = new ENTITY.Clientes();
                clientes.Lista_Clientes = DAL.Clientes.Clientes.ObtenerClientesPendientes();
                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.Clientes));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Clientes.xml", clientes, serialiser);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
