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
        public string Fecha { get; set; }

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
        public string Fecha_transmision { get; set; }


    }

    [XmlRootAttribute(ElementName = "detalle_articulos", IsNullable = false)]
    public class AjusteInventarios
    {
        [XmlElement("ajuste_inventario")]
        public List<AjusteInventario> Lista_ajusteInventario { get; set; }
    }
}

