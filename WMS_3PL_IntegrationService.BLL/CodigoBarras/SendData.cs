﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.BLL.CodigoBarras
{
    public class SendData
    {
        public static void SendWMS_3PLCodigoBarras()
        {
            try
            {
                ENTITY.CodigoArticulos codigoArticulos = new ENTITY.CodigoArticulos();
                codigoArticulos.Lista_Codigos = DAL.Articulos.Articulos.ObtenerCodArticulosPendientes();

                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.CodigoArticulos));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Codigos de Barras.xml", codigoArticulos, serialiser);

                UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\", "Codigos de Barras.xml");

            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                DAL.Herramientas.GuardarError(new ENTITY.Errores.Errores("Libreria: BLL - Clase: SendData - Metodo: SendWMS_3PLCodigoBarras", mensaje.ToString()));

            }


        }
    }
}
