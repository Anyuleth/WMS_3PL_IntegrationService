﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.BLL.Precios
{
    public class SendData
    {
        public static void SendWMS_3PLPrecios()
        {
            try
            {
                ENTITY.Precios.Precios precios = new ENTITY.Precios.Precios();
                precios.Lista_Precios = DAL.Precios.Precios.ObtenerPreciosPendientes();
                XmlSerializer serialiser = new XmlSerializer(typeof(ENTITY.Precios.Precios));

                UTILITY.XML.CreateXML(@"C:\Program Files (x86)\AR Holdings\3PL\Precios.xml", precios, serialiser);

                // UTILITY.SFTP.SendSFTP(@"C:\Program Files (x86)\AR Holdings\3PL\Articulos.xml");

            }
            catch (Exception ex)
            {


            }


        }
    }
}
