using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY.AjusteInventario
{
    public class AjusteInventario
    {
        [XmlElement("empresa")]
        public string Empresa { get; set; }

        [XmlElement("fecha")]
        public DateTime Fecha { get; set; }

        [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("bodega")]
        public string Bodega { get; set; }

        [XmlElement("cantidad")]
        public string Cantidad { get; set; }

        [XmlElement("tipo")]
        public string Tipo { get; set; }

        [XmlElement("comentarios")]
        public string Comentarios { get; set; }

        [XmlElement("fecha_transmision")]
        public DateTime Fecha_transmision { get; set; }


    }

    [XmlRootAttribute(ElementName = "ajuste_inventario", IsNullable = false)]
    public class AjusteInventarios
    {
        [XmlElement("detalle_articulos")]
        public List<AjusteInventario> Lista_ajusteInventario { get; set; }
    }
}

