using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.UTILITY
{
    public class XML
    {
      
        public static void CreateXML(string fileName, object objectName, XmlSerializer serialiser)
        {

            TextWriter Filestream = new StreamWriter(fileName);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serialiser.Serialize(Filestream, objectName, ns);

            Filestream.Close();


        }
    }
}
