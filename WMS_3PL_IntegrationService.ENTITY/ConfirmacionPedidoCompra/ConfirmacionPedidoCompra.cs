using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY.ConfirmacionPedidoCompra
{
    public class ConfirmacionPedidoCompra
    {
        [XmlElement("linea_numero")]
        public string Linea_Numero { get; set; }

        [XmlElement("empresa")]
        public string Empresa { get; set; }

        [XmlElement("documento")]
        public string Documento { get; set; }

        [XmlElement("factura")]
        public string Factura { get; set; }

        [XmlElement("tipo")]
        public string Tipo { get; set; }

        [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("bodega")]
        public string Bodega { get; set; }

        [XmlElement("cantidad")]
        public decimal Cantidad { get; set; }

        [XmlElement("fecha_transmision")]
        public DateTime Fecha_Transmision { get; set; }
    }

    [XmlRootAttribute(ElementName = "confirmacion_pedido_compra", IsNullable = false)]
    public class ConfirmacionPedidoCompras
    {
    [XmlElement("articulo")]
    public List<ConfirmacionPedidoCompra> Lista_articulos { get; set; }
    }
}
