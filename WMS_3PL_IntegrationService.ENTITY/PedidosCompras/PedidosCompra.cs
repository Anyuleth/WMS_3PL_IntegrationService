using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY.PedidosCompras
{
    [XmlRootAttribute(ElementName = "pedido_compra", IsNullable = false)]
    public class PedidosCompra
    {
        [XmlElement("empresa")]
        public string Empresa { get; set; }

        [XmlElement("documento")]
        public string Documento { get; set; }

        [XmlElement("tipo")]
        public string Tipo { get; set; }

        [XmlElement("fecha_transmision")]
        public DateTime Fecha_transmision { get; set; }

        [XmlElement("fecha_entrada")]
        public string Fecha_Entrada { get; set; }

        [XmlElement("detalle")]
        public List<Lineas> Linea { get; set; }
    }
   
   
    public class Lineas
    {
        [XmlElement("linea_numero")]
        public int linea_Numero { get; set; }

        [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("bodega")]
        public string Bodega { get; set; }

        [XmlElement("cantidad")]
        public decimal Cantidad { get; set; }


    }

    
}
