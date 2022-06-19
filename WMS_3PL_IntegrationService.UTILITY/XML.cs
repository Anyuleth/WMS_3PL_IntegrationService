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
      

        public static void CreateXML(string fileName, object objectName, XmlSerializer serializer)
        {
           

            TextWriter Filestream = new StreamWriter(fileName);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(Filestream, objectName, ns);

            Filestream.Close();


        }

        public static T DeserializeToObject<T>(string filepath) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(filepath))
            {
                return (T)ser.Deserialize(sr);
            }
        }
      
    }
}
