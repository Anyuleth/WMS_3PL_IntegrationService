using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY.ConciliacionInventarios
{
    
    public class ConciliacionInventario
    {
        [XmlElement("empresa")]
        public string Empresa { get; set; }

        [XmlElement("fecha")]
        public DateTime Fecha { get; set; }

        [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("bodega")]
        public string Bodega { get; set; }

        [XmlElement("total")]
        public string Total { get; set; }

        [XmlElement("disponible")]
        public string Disponible { get; set; }


        [XmlElement("fecha_transmision")]
        public DateTime Fecha_transmision { get; set; }


    }

    [XmlRootAttribute(ElementName = "conciliacion_inventario", IsNullable = false)]
    public class ConciliacionInventarios
    {
        [XmlElement("detalle_articulos")]
        public List<ConciliacionInventario> Lista_ConciliacionInventario { get; set; }
    }
}
