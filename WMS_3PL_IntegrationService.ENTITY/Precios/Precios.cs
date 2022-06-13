using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY.Precios
{
    public class PreciosU
    {
        [XmlElement("empresa")]
        public string Empresa { get; set; }

       [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("precio")]
        public decimal Precio { get; set; }

        [XmlElement("moneda")]
        public string Moneda { get; set; }

        [XmlElement("pais")]
        public string Pais { get; set; }

        [XmlElement("fecha_transmision")]
        public DateTime Fecha_Transmision { get; set; }
    }

    [XmlRootAttribute(ElementName = "lista_precios", IsNullable = false)]
    public class Precios
    {
        [XmlElement("precio_articulo")]
        public List<PreciosU> Lista_Precios { get; set; }
    }
}
