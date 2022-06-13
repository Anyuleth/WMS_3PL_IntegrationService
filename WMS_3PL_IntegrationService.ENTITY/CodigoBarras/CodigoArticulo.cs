using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY
{

    public class CodigoArticulo
    {
        [XmlElement("empresa")]
        public string Empresa { get; set; }

        [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("codigo")]
        public string Codigo { get; set; }

        [XmlElement("fecha_transmision")]
        public DateTime Fecha_Transmision { get; set; }
    }

    [XmlRootAttribute(ElementName = "lista_codigos", IsNullable = false)]
    public class CodigoArticulos
    {
        [XmlElement("codigo_articulo")]
        public List<CodigoArticulo> Lista_Codigos { get; set; }
    }

    
}

