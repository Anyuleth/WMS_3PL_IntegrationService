using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY
{
  
    public class Cliente
    {
        [XmlElement("empresa")]
        public string empresa { get; set; }

        [XmlElement("numero")]
        public string numero { get; set; }

        [XmlElement("bodega")]
        public string bodega { get; set; }

        [XmlElement("dir1")]
        public string dir1 { get; set; }

        [XmlElement("nombre")]
        public string nombre { get; set; }

        [XmlElement("pais")]
        public string pais { get; set; }

        [XmlElement("fecha_transmision")]
        public DateTime fecha_transmision { get; set; }
    }

    [XmlRootAttribute(ElementName = "lista_clientes", IsNullable = false)]
    public class Clientes
    {
        [XmlElement("cliente")]
        public List<Cliente> Lista_Clientes { get; set; }
    }

}
